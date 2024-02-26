using MediatR;

namespace Dotnet.Sales.Application.SalesPerson.Queries
{
    public sealed record SalesPersonQueryAllRequest : IRequest<IEnumerable<SalesPersonQueryListResponse>> { }
}
