namespace FlightsApi.Domain.Entities
{
    public record Booking(
        Guid FlightId,
        string PassengerEmail,
        int NumberOfSeats
    );

}