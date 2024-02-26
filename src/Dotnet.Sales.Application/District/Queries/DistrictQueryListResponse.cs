namespace Dotnet.Sales.Application.District.Queries
{
    public sealed record DistrictQueryListResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }
}
