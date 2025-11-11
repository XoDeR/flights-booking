namespace FlightsApi.ReadModels
{
    // Read Models are used for transferring information
    public record FlightRm(
        Guid Id,
        string Airline,
        string Price,
        TimePlaceRm Departure,
        TimePlaceRm Arrival,
        int RemainingNumberOfSeats
        );
}