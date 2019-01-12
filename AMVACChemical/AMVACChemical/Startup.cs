using AMVACChemical.Interfaces.Services;
using AMVACChemical.Services.TrackAbout;
using AMVACChemical.ViewModels.TrackAbout;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMVACChemical
{
    public class Startup
    {
        // Declare global variables
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        // create constructor
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"config.{_env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets("aspnet-ER3.Web.App-716a052e-4235-4e5f-93bf-ba57ba6934b9");
            }
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            services.AddDbContext<AMVAC_TrackaboutContext>(
             options => options.UseSqlServer(_config["ConnectionStrings:LocalContextConnection"])
             );

            //Add Token Validatitors
            var tokenParams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = "*",
                ValidAudience = "*",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TrackAboutApi:ApiKey"]))
            };

            //Add Jwt Authentication 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtConfig => {
                JwtConfig.TokenValidationParameters = tokenParams;
            });

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                }
            });

            //registered Global Exception Handler class
            ILoggerFactory loggerFactory = new LoggerFactory();
            ConfigureGlobal(services, loggerFactory);

            services.AddEntityFrameworkSqlServer().AddDbContext<AMVAC_TrackaboutContext>();
            services.AddScoped<ITrackAboutServices, TrackAboutService>();

            // add logging:
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();
            loggerFactory.AddFile("Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile("Logs/Error-{Date}.txt", LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            // integrate cors for cross integration
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller}/{action}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "TrackAbout", action = "Index" }
                    );
            });
        }

        /// <summary>
        /// Method used to configure global level configurations : exception handling
        /// </summary>
        /// <param name="services"></param>
        /// <param name="factory"></param>
        private void ConfigureGlobal(IServiceCollection services, ILoggerFactory factory)
        {
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddMvcOptions(o => o.Filters.Add(new GlobalExceptionLogger(factory)));

        }
    }
}
