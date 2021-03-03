using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> logger;
        private readonly IConfiguration configuration;

        public TemperatureController(ILogger<TemperatureController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

        }

        [HttpGet]
        [Route("")]
        public ActionResult<Dictionary<string, string>> Get()
        {   
            return Ok();
        }
    }
}