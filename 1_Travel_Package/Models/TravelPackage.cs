namespace TravelPackageApi.Models
{
    public record TravelPackage(
        int PackageId,
        string Destination,
        DateTime StartDate,
        DateTime EndDate,
        decimal Price,
        string? Description
    );
}
