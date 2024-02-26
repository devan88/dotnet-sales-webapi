using Asp.Versioning;
using Dotnet.Sales.Application.SalesPerson.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Sales.WebApi.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public sealed class SalesPersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DistrictController> _logger;

        public SalesPersonController(
            IMediator mediator,
            ILogger<DistrictController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<SalesPersonQueryListResponse>> GetAllSalesPersons()
        {
            var request = new SalesPersonQueryAllRequest();
            var result = await _mediator.Send(request);
            return result;
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<SalesPersonQueryListResponse>> GetSalesPersons([FromQuery] int[] salesPersonIds)
        {
            var request = new SalesPersonQueryListRequest() { Ids = salesPersonIds };
            var result = await _mediator.Send(request);
            return result;
        }
    }
}
