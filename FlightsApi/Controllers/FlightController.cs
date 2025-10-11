using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Search()
            => new string[]
            {
                "American Airlines",
                "British Airways",
                "Lufthansa",
            };
    }
}
