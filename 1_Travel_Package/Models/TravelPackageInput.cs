namespace TravelPackageApi.Models
{
    public record TravelPackageInput(
        string Destination,
        DateTime StartDate,
        DateTime EndDate,
        decimal Price,
        string? Description
    );
}
