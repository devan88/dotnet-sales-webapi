using MediatR;

namespace Dotnet.Sales.Application.District.Commands
{
    public sealed record UpdateDistrictCommandRequest : IRequest<bool>
    {
        public int DistrictId { get; init; }
        public int PrimarySalesPersonId { get; init; }
        public int[] SecondarySalesPersonIds { get; init; }

        public UpdateDistrictCommandRequest(int districtId, int primarySalesPersonId, int[] secondarySalesPersonIds)
        {
            DistrictId = districtId;
            PrimarySalesPersonId = primarySalesPersonId;
            SecondarySalesPersonIds = secondarySalesPersonIds;
        }
    }
}
