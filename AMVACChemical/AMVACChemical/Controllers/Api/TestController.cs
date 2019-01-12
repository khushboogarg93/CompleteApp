using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMVACChemical.Controllers.Api
{
    public class TestController : Controller
    {
        private readonly ILogger _logger;

     
        public TestController(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<TestController>();
        }     
       
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {           
            throw new InvalidOperationException("exception raised globally");           
        }      
    }
}
