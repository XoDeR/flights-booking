namespace FlightsApi.Domain.Entities
{
    public record Booking(
        string PassengerEmail,
        int NumberOfSeats
    );

}