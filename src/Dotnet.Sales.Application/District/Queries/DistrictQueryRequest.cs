using MediatR;

namespace Dotnet.Sales.Application.District.Queries
{
    public sealed record DistrictQueryRequest : IRequest<DistrictQueryResponse>
    {
        public int DistrictId { get; init; }

        public DistrictQueryRequest(int districtId)
        {
            DistrictId = districtId;
        }
    }
}
