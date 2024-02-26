namespace Dotnet.Sales.Application.District.Queries
{
    public sealed record DistrictQueryResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public int PrimarySalesPersonId { get; init; }
        public required IEnumerable<int> SecondarySalesPersonIds { get; init; }
        public IEnumerable<string>? Stores { get; init; }
    }
}
