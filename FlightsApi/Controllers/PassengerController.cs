using Microsoft.AspNetCore.Mvc;
using FlightsApi.Dtos;
using FlightsApi.Data;
using FlightsApi.ReadModels;
using FlightsApi.Domain.Entities;

namespace FlightsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        static private readonly Entities Entities = new Entities();

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            Entities.Passengers.Add(new Passenger(
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
            ));
            //System.Diagnostics.Debug.WriteLine(Passengers.Count);
            Console.WriteLine(Entities.Passengers.Count);
            return CreatedAtAction(nameof(Find), new { email = dto.Email }, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = Entities.Passengers.FirstOrDefault(p => p.Email == email);

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
