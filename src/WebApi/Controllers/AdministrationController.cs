using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<AdministrationController> logger;
        private readonly IConfiguration configuration;

        public AdministrationController(ILogger<AdministrationController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

        }

        [HttpGet]
        [Route("RuntimeConf")]
        public ActionResult<Dictionary<string, string>> GetRuntimeConfiguration()
        {
            var confs = new Dictionary<string, string>();
            var environment = this.configuration["ENVIRONMENT"];

            confs.Add("Environment", environment);

            var defaultConnectionString = this.configuration.GetConnectionString("Default");
            var customConnectionString = this.configuration.GetConnectionString("MyCustomConnectionString");


            confs.Add("Default ConnectionString", defaultConnectionString);
            confs.Add("Custom ConnectionString", customConnectionString);
            
            if (confs.Count < 1)
            {
                return NotFound();
            }
            
            return Ok(confs);
        }
    }
}