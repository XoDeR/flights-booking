namespace FlightsApi.Dtos
{
    public record BookDto(
        Guid FlightId,
        string PassengerEmail,
        int NumberOfSeats
    );

}