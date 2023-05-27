using MediatR;
using NewShore.Travel.Domain.Features.Entities;
using NewShore.Travel.Domain.Features.Flights.Contracts.Integrations;
using NewShore.Travel.Domain.Features.Flights.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination
{
    public class GetFlightsByOriginAndDestinationQueryHandler : IRequestHandler<GetFlightsByOriginAndDestinationQuery,List<Journey>>
    {
        private readonly IFlightsService _flightsService;

        public GetFlightsByOriginAndDestinationQueryHandler(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        public async Task<List<Journey>> Handle(GetFlightsByOriginAndDestinationQuery request, CancellationToken cancellationToken)
        {
            var flights = await _flightsService.GetFlights();
            var routes = FindFlights(flights, request.Origin, request.Destination, request.MaxNumberFlights);
            if (!routes.Any())
                throw new Exception(ValidationMessages.RouteFlightUnavailable);
            var journeys = routes.Select(route=> new Journey(request.Origin, request.Destination)
            {
                Flights = route,
                Price = route.Sum(flight => flight.Price)
            })
            .OrderBy(journey=> journey.Price)
            .ToList();
            return journeys;
        }

        private List<List<Flight>> FindFlights(List<Flight> flights, string origin, string destination,int maxNumberFlights)
        {
            var routes = new List<List<Flight>>();
            var currentRoute = new List<Flight>();

            void DFS(Flight currentFlight, int depth)
            {
                currentRoute.Add(currentFlight);

                if (currentFlight.Destination == destination && depth <= maxNumberFlights)
                {
                    routes.Add(new List<Flight>(currentRoute));
                }
                else if (depth < maxNumberFlights)
                {
                    var nextFlights = flights.Where(f => f.Origin == currentFlight.Destination
                        && !currentRoute.Any(route => route.Origin == currentFlight.Destination));

                    foreach (var nextFlight in nextFlights)
                    {
                        DFS(nextFlight, depth + 1);
                    }
                }

                currentRoute.RemoveAt(currentRoute.Count - 1);
            }

            var initialFlights = flights.Where(f => f.Origin == origin);
            foreach (var initialFlight in initialFlights)
            {
                DFS(initialFlight, 1);
            }
            return routes;
        }
    }
}
