namespace Dotnet.Sales.Application.SalesPerson.Queries
{
    public sealed record SalesPersonQueryListResponse
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }
}
