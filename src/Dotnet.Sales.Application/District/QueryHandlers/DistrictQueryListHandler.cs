using AutoMapper;
using Dotnet.Sales.Application.District.Queries;
using MediatR;

namespace Dotnet.Sales.Application.District.QueryHandlers
{
    public sealed class DistrictQueryListHandler : IRequestHandler<DistrictQueryListRequest, IEnumerable<DistrictQueryListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDistrictQueryRepository _districtQueryRepository;

        public DistrictQueryListHandler(
            IMapper mapper,
            IDistrictQueryRepository districtQueryRepository)
        {
            _mapper = mapper;
            _districtQueryRepository = districtQueryRepository;
        }

        public async Task<IEnumerable<DistrictQueryListResponse>> Handle(DistrictQueryListRequest request, CancellationToken cancellationToken)
        {
            var result = await _districtQueryRepository.GetAllDistrictAsync(cancellationToken);
            var response = _mapper.Map<IEnumerable<DistrictQueryListResponse>>(result);
            return response;
        }
    }
}
