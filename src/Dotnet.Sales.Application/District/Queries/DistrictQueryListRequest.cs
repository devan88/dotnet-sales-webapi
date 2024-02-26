using MediatR;

namespace Dotnet.Sales.Application.District.Queries
{
    public sealed record DistrictQueryListRequest : IRequest<IEnumerable<DistrictQueryListResponse>> { }
}
