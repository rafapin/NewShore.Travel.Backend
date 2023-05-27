using MediatR;
using NewShore.Travel.Domain.Features.Entities;
using System.Collections.Generic;

namespace NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination
{
    public class GetFlightsByOriginAndDestinationQuery : IRequest<List<Journey>>
    {
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public int MaxNumberFlights { get; set; }
    }
}
