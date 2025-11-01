using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightsApi.Dtos;
using FlightsApi.ReadModels;

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
            return CreatedAtAction(nameof(Find), new { email = dto.Email }, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = Passengers.FirstOrDefault(p => p.Email == email);

            if (passenger == null)
            {
                return NotFound();
            }

            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
            );

            return Ok(rm);
        }
    }
}
