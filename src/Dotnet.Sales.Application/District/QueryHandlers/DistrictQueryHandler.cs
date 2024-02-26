using AutoMapper;
using Dotnet.Sales.Application.District.Queries;
using MediatR;

namespace Dotnet.Sales.Application.District.QueryHandlers
{
    public sealed class DistrictQueryHandler : IRequestHandler<DistrictQueryRequest, DistrictQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictQueryRepository _districtQueryRepository;

        public DistrictQueryHandler(
            IMapper mapper,
            IDistrictQueryRepository districtQueryRepository)
        {
            _mapper = mapper;
            _districtQueryRepository = districtQueryRepository;
        }

        public async Task<DistrictQueryResponse> Handle(DistrictQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _districtQueryRepository.GetDistrictAsync(request.DistrictId, cancellationToken);
            var response = _mapper.Map<DistrictQueryResponse>(result);
            return response;
        }
    }
}
