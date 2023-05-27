using NewShore.Travel.Domain.Features.Entities;
using System.Collections.Generic;

namespace NewShore.Travel.Domain.Features.Flights.Contracts.Integrations
{
    public interface IFlightsService
    {
        List<Flight> GetFlights(string origin, string destination); 
    }
}
