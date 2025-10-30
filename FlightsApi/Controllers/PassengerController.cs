using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightsApi.Dtos;

namespace FlightsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        static private IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Passengers.Add(dto);
            //System.Diagnostics.Debug.WriteLine(Passengers.Count);
            Console.WriteLine(Passengers.Count);
            return Ok();
        }
    }
}
