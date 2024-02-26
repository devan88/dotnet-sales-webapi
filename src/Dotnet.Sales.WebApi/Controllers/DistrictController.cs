using Asp.Versioning;
using Dotnet.Sales.Application.District.Commands;
using Dotnet.Sales.Application.District.Queries;
using Dotnet.Sales.WebApi.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Sales.WebApi.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    public sealed class DistrictController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DistrictController> _logger;

        public DistrictController(
            IMediator mediator,
            ILogger<DistrictController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[MapToApiVersion(2)]
        [HttpGet]
        public async Task<IEnumerable<DistrictQueryListResponse>> GetAllDistricts()
        {
            var request = new DistrictQueryListRequest();
            var result = await _mediator.Send(request);
            return result;
        }

        [HttpGet]
        [Route("{districtId}")]
        public async Task<DistrictQueryResponse> GetDistrict(int districtId)
        {
            var request = new DistrictQueryRequest(districtId);
            var result = await _mediator.Send(request);
            return result;
        }

        [HttpPut]
        [Route("{districtId}")]
        public async Task<NoContentResult> UpdateDistrict(UpdateDistrictViewModel model)
        {
            _logger.LogDebug("UpdateDistrict request {@model}", model);
            var request = new UpdateDistrictCommandRequest(
                model.DistrictId,
                model.PrimarySalesPersonId,
                model.SecondarySalesPersonIds);
            await _mediator.Send(request);
            return new NoContentResult();
        }
    }
}
