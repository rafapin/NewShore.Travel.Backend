using NewShore.Travel.Domain.Features.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewShore.Travel.Domain.Features.Flights.Contracts.Integrations
{
    public interface IFlightsService
    {
        Task<List<Flight>> GetFlights(); 
    }
}
