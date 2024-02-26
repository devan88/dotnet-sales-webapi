using MediatR;

namespace Dotnet.Sales.Application.SalesPerson.Queries
{
    public sealed record SalesPersonQueryListRequest : IRequest<IEnumerable<SalesPersonQueryListResponse>>
    {
        public int[] Ids { get; init; } = Array.Empty<int>();
    }
}
