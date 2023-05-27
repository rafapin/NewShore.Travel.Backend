using MediatR;
using NewShore.Travel.Domain.Features.Entities;
using NewShore.Travel.Domain.Features.Flights.Contracts.Integrations;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination
{
    public class GetFlightsByOriginAndDestinationQueryHandler : IRequestHandler<GetFlightsByOriginAndDestinationQuery,Journey>
    {
        private readonly IFlightsService _flightsService;

        public GetFlightsByOriginAndDestinationQueryHandler(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        public async Task<Journey> Handle(GetFlightsByOriginAndDestinationQuery request, CancellationToken cancellationToken)
        {
            var journey = await _flightsService.GetFlights(request.Origin, request.Destination);
            var firstFlight = GetCheapFlight(journey.Flights, request.Origin, request.Destination);
            var secondFlight = GetCheapFlight(journey.Flights, request.Origin, request.Destination);
            journey.Flights = new List<Flight> { firstFlight, secondFlight };
            journey.Price = firstFlight.Price + secondFlight.Price;
            return journey;
        }

        private Flight GetCheapFlight(List<Flight> flights,string origin,string destination)
        {
            return flights.Where(flight => flight.Origin == origin && flight.Destination == destination)
                    .OrderBy(flight => flight.Price)
                    .FirstOrDefault();
        }
    }
}
