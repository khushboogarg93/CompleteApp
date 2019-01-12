using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMVACChemical
{
    public class GlobalExceptionLogger : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionLogger(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<GlobalExceptionLogger>();
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message);
            var viewResult = new ViewResult();
            viewResult.ViewName = Resources.TrackAboutResource.errorPath;
            context.Result = viewResult;
        }
    }
}
