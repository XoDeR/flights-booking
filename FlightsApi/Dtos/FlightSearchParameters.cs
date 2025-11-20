using System.ComponentModel;

namespace FlightsApi.Dtos
{
    public record FlightSearchParameters(
        [DefaultValue("12/25/2024 11:40:00 AM")]
        DateTime? FromDate,
        [DefaultValue("12/27/2024 11:40:00 AM")]
        DateTime? ToDate,
        [DefaultValue("Los Angeles")]
        string? From,
        [DefaultValue("Berlin")]
        string? Destination,
        [DefaultValue(1)]
        int? NumberOfPassengers
    );
}