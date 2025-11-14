using Microsoft.AspNetCore.Mvc;
using FlightsApi.Data;
using FlightsApi.ReadModels;

namespace FlightsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly Entities _entities;

        public BookingController(Entities entities)
        {
            _entities = entities;
        }
    }
}