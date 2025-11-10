namespace FlightsApi.Domain.Entities
{
    public record FlightRm(
        Guid Id,
        string Airline,
        string Price,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int RemainingNumberOfSeats
        );
}