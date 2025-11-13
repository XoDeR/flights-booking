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
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _entities.Passengers.Add(new Passenger(
                Guid.NewGuid(),
                dto.Email,
                dto.FirstName,
                dto.LastName,
                dto.Gender
            ));
            //System.Diagnostics.Debug.WriteLine(Passengers.Count);
            Console.WriteLine(_entities.Passengers.Count());
            return CreatedAtAction(nameof(Find), new { email = dto.Email }, dto);
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(p => p.Email == email);

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
