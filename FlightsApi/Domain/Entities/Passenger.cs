namespace FlightsApi.Domain.Entities
{
    public record Passenger(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        bool Gender);
}