using MediatR;
using NewShore.Travel.Domain.Features.Entities;

namespace NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination
{
    public class GetFlightsByOriginAndDestinationQuery : IRequest<Journey>
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
    }
}
