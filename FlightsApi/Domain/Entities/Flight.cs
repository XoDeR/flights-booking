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
        );
}