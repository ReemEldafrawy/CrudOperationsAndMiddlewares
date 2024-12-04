using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace CrudOperations.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConfigController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<AttatchmentsOptions> _attackmentsOptions;
        public ConfigController(IConfiguration configuration,IOptionsMonitor<AttatchmentsOptions> attatchmentsOptions)
        {
            _configuration = configuration;
            _attackmentsOptions = attatchmentsOptions;
            var Value = _attackmentsOptions.CurrentValue;
            
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetConfigurations()
        {
            Thread.Sleep(10000);
            var configrations = new
            {
                Evrname = _configuration["ASPNETCORE_ENVIRONMENT"],
                AllowedHosts = _configuration["AllowedHosts"],
                DefaultConnection = _configuration.GetConnectionString("DefaultConnection"),
                DefaultLogLevel = _configuration["Logging:LogLevel:Default"],
                TestKey = _configuration["TestKey"],
                SigningId = _configuration["SigningId"],
                pass = _configuration["pass"],
                Maxsize = _attackmentsOptions.CurrentValue
              
            };
            return Ok(configrations);
        }





    }
}
