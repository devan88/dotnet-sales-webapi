using AutoMapper;
using Dotnet.Sales.Application.SalesPerson.Queries;
using MediatR;

namespace Dotnet.Sales.Application.SalesPerson.QueryHandlers
{
    public sealed class SalesPersonQueryListHandler : IRequestHandler<SalesPersonQueryListRequest, IEnumerable<SalesPersonQueryListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ISalesPersonQueryRepository _salesPersonQueryRepository;

        public SalesPersonQueryListHandler(
            IMapper mapper,
            ISalesPersonQueryRepository salesPersonQueryRepository)
        {
            _mapper = mapper;
            _salesPersonQueryRepository = salesPersonQueryRepository;
        }

        public async Task<IEnumerable<SalesPersonQueryListResponse>> Handle(SalesPersonQueryListRequest request, CancellationToken cancellationToken)
        {
            var result = await _salesPersonQueryRepository.GetSalesPersonsAsync(request.Ids, cancellationToken);
            var response = _mapper.Map<IEnumerable<SalesPersonQueryListResponse>>(result);
            return response;
        }
    }
}
