using FlightsApi.ReadModels;
using Microsoft.AspNetCore.Mvc;
using FlightsApi.Dtos;
using FlightsApi.Domain.Errors;
using FlightsApi.Data;

namespace FlightsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly Entities _entities;

        private readonly ILogger<FlightController> _logger;

        public FlightController(ILogger<FlightController> logger,
            Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(500)] // internal server error
        [ProducesResponseType(typeof(FlightRm), 200)]
        [HttpGet("{id}")] // param needs to be specified
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(
                    flight.Departure.Place.ToString(),
                    flight.Departure.Time
                ),
                new TimePlaceRm(
                    flight.Arrival.Place.ToString(),
                    flight.Arrival.Time
                ),
                flight.RemainingNumberOfSeats
            );

            return Ok(flight);
        }

        [HttpGet]
        //[HttpGet("/Flight")]
        //[SwaggerOperation(OperationId = "getFlights")]
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(500)] // internal server error
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRmList = _entities.Flights.Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(
                    flight.Departure.Place.ToString(),
                    flight.Departure.Time
                ),
                new TimePlaceRm(
                    flight.Arrival.Place.ToString(),
                    flight.Arrival.Time
                ),
                flight.RemainingNumberOfSeats
            )).ToArray();

            return flightRmList;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(500)] // internal server error
        [ProducesResponseType(200)] // success
        public IActionResult Book(BookDto dto)
        {
            Console.WriteLine($"Booking a new flight {dto.FlightId}");

            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
            {
                return NotFound();
            }

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
            {
                return Conflict(new { message = "The number of requested seats exceeds the number of remainining seats" }); // 409
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}
