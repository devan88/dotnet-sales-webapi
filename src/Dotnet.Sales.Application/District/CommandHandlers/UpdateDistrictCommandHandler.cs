using Dotnet.Sales.Application.District.Commands;
using MediatR;

namespace Dotnet.Sales.Application.District.CommandHandlers
{
    public sealed class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommandRequest, bool>
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IDistrictQueryRepository _districtQueryRepository;

        public UpdateDistrictCommandHandler(
            IDistrictQueryRepository districtQueryRepository,
            IDistrictRepository districtRepository)
        {
            _districtQueryRepository = districtQueryRepository;
            _districtRepository = districtRepository;
        }

        public async Task<bool> Handle(UpdateDistrictCommandRequest request, CancellationToken cancellationToken)
        {
            var district = await _districtQueryRepository.GetDistrictAsync(request.DistrictId, cancellationToken);
            if(district is null)
            {
                throw new DomainException($"District id: {request.DistrictId} was not found");
            }
            district.UpdateSalesPerson(request.PrimarySalesPersonId, request.SecondarySalesPersonIds);
            return await _districtRepository.UpdateSalesPersonAsync(district, cancellationToken);
        }
    }
}
