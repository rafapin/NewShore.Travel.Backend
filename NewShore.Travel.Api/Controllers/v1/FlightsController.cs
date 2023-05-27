using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination;

namespace NewShore.Travel.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FlightsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetFlights([FromQuery] GetFlightsByOriginAndDestinationQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}