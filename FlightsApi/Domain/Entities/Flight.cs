using FlightsApi.Domain.Errors;

namespace FlightsApi.Domain.Entities
{
    // Entities are used for storing data (in db)
    public record Flight(
        Guid Id,
        string Airline,
        string Price,
        TimePlace Departure,
        TimePlace Arrival,
        int RemainingNumberOfSeats
        )
    {
        public IList<Booking> Bookings = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;

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