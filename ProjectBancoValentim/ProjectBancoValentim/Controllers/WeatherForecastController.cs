using Microsoft.AspNetCore.Mvc;

namespace ProjectBancoValentim.Controllers
{
    [ApiController]
    [Route("api/viniboy")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("exemplo")]
        public string Get()
        {
            return "vINIBOY";
        }

        [HttpPost("exemplo2")]
        public string Post()
        {
            return "LivinhaDaProgramção <3";
        }

        [HttpPut("exemplo3")]

        public string Put()

        {
            return "Eu amo minha namroada <3";
        }
    }
}