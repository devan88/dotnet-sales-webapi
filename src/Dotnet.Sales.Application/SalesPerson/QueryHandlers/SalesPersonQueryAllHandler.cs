using AutoMapper;
using Dotnet.Sales.Application.SalesPerson.Queries;
using MediatR;

namespace Dotnet.Sales.Application.SalesPerson.QueryHandlers
{
    public sealed class SalesPersonQueryAllHandler : IRequestHandler<SalesPersonQueryAllRequest, IEnumerable<SalesPersonQueryListResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ISalesPersonQueryRepository _salesPersonQueryRepository;

        public SalesPersonQueryAllHandler(
            IMapper mapper,
            ISalesPersonQueryRepository salesPersonQueryRepository)
        {
            _mapper = mapper;
            _salesPersonQueryRepository = salesPersonQueryRepository;
        }

        public async Task<IEnumerable<SalesPersonQueryListResponse>> Handle(SalesPersonQueryAllRequest request, CancellationToken cancellationToken)
        {
            var result = await _salesPersonQueryRepository.GetAllSalesPersonsAsync(cancellationToken);
            var response = _mapper.Map<IEnumerable<SalesPersonQueryListResponse>>(result);
            return response;
        }
    }
}
