using AMVACChemical.Interfaces.Services;
using AMVACChemical.ViewModels.TrackAbout;
using AMVACChemical.ViewModels.TrackAbout.Assets;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.AssetTransferLocation;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Customers;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.Fill;
using AMVACChemical.ViewModels.TrackAbout.RequestParam.TransferAssetToCustomer;
using AMVACChemical.ViewModels.TrackAbout.ResponseParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMVACChemical.Controllers.Api
{
    public class TrackAboutApiController : Controller
    {

        // Declare global configurations for inject
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;
        private readonly ITrackAboutServices _trackAboutService;
        private readonly ILogger<TrackAboutApiController> _logger;

        // Create constructor
        public TrackAboutApiController(IHostingEnvironment env, IConfigurationRoot config, ITrackAboutServices trackAboutService,
            ILogger<TrackAboutApiController> logger)
        {
            _env = env;
            _config = config;
            _trackAboutService = trackAboutService;
            _logger = logger;
        }

        #region-- Assets
        /// <summary>
        /// Method used to get a list of all assets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetAssetsList()
        {
            AssetsVM assetsDetail = new AssetsVM();
            try
            {
                assetsDetail = await _trackAboutService.GetAssetList();
                return Json(assetsDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new AssetsVM());
            }
        }

        /// <summary>
        /// This method is used for Login the Authenticated user by Email and Password
        /// </summary>
        /// <returns></returns>

        [HttpPost]
   
        public JsonResult Login([FromBody]LoginVM login)
        {
            IActionResult response = Unauthorized();
         
            if (login.Username == _config["TrackAboutApi:Username"] && login.Password== _config["TrackAboutApi:Password"])
            {

                var tokenString = GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
            }
            
            return Json(response);
        }
        /// <summary>
        /// This method is used for creating the token  
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(LoginVM login)
        {
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TrackAboutApi:ApiKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);
            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, login.Username)
    
    };

            var token = new JwtSecurityToken(_config["*"],
              _config["*"],
              null,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    /// <summary>
    /// MoveAssetToCustomer method is use to move asset to customer.
    /// </summary>
    /// <param name="deliveriesVM"></param>
    /// <returns></returns>
    [HttpPost]
        public async Task<JsonResult> MoveAssetToCustomer([FromBody]DeliveriesVM deliveriesVM)
        {
            try
            {
                DeliveriesResponseVM deliveriesResponseVM = new DeliveriesResponseVM();
                deliveriesResponseVM = await _trackAboutService.MoveAssetToCustomer(deliveriesVM);
                return Json(deliveriesResponseVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new DeliveriesResponseVM());
            }
        }

        /// <summary>
        /// Method  used to get a detail of  From Branch to Branch Locations 
        /// </summary>
        /// <param name="assetTransferVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> MoveAssetLocation([FromBody]AssetTransferVM assetTransferVM)
        {
            try
            {

                AssetLocationResponseVM assetTransferResult = new AssetLocationResponseVM();
                // Method used to Move Asset Location 
                assetTransferResult = await _trackAboutService.MoveAssetLocation(assetTransferVM);
                return Json(assetTransferResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new AssetTransferVM());
            }
        }

        /// <summary>
        /// Fill method is used to notify TrackAbout that an Asset (SmartCartridge) has been filled with a product (chemical). 
        /// </summary>
        /// <param name="fillsVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Fill([FromBody]FillsVM fillsVM)
        {
            try
            {
                FillsResponseVM fillsResponseVM = new FillsResponseVM();
                fillsResponseVM = await _trackAboutService.Fill(fillsVM);
                return Json(fillsResponseVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new FillsResponseVM());
            }
        }
        #endregion

        #region--Location
        /// <summary>
        /// Method used to get a list of all locations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetListOfAllLocation()
        {
            try
            {
                ListofallLocation listofAllLocation = new ListofallLocation();
                listofAllLocation = await _trackAboutService.GetListOfAllLocation();
                return Json(listofAllLocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new ListofallLocation());
            }

        }
        #endregion

        #region--Customers
        /// <summary>
        /// Method used to get list of all visible customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetListOfAllVisibleCustomer()
        {
            try
            {
                CustomerAllVisibleList customerAllVisibleList = new CustomerAllVisibleList();
                customerAllVisibleList = await _trackAboutService.GetListOfAllVisibleCustomer();
                return Json(customerAllVisibleList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new CustomerAllVisibleList());
            }
        }
        #endregion
    }
}
