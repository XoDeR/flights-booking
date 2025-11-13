using FlightsApi.Domain.Errors;

namespace FlightsApi.Domain.Entities
{
    // Entities are used for storing data (in db)
    public record Flight
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Airline { get; init; } = string.Empty;
        public string Price { get; init; } = string.Empty;
        public TimePlace Departure { get; init; } = new TimePlace(string.Empty, DateTime.MinValue);
        public TimePlace Arrival { get; init; } = new TimePlace(string.Empty, DateTime.MinValue);
        public int RemainingNumberOfSeats { get; set; } = 0;

        public Flight() { }
        public Flight(Guid id, string airline, string price, TimePlace departure, TimePlace arrival, int remainingSeats)
        {
            Id = id;
            Airline = airline;
            Price = price;
            Departure = departure;
            Arrival = arrival;
            RemainingNumberOfSeats = remainingSeats;
        }
        public IList<Booking> Bookings = new List<Booking>();

        // internal means the method is used inside the same assembly
        internal object? MakeBooking(string passengerEmail, int numberOfSeats)
        {
            var flight = this;

            // Example of domain rules validation
            if (flight.RemainingNumberOfSeats < numberOfSeats)
            {
                return new OverbookError();
            }

            flight.Bookings.Add(
                new Booking(
                    passengerEmail,
                    numberOfSeats
                )
            );

            flight.RemainingNumberOfSeats -= numberOfSeats;
            return null;
        }
    }
}