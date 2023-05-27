using NewShore.Travel.Domain.Features.Entities;
using System.Threading.Tasks;

namespace NewShore.Travel.Domain.Features.Flights.Contracts.Integrations
{
    public interface IFlightsService
    {
        Task<Journey> GetFlights(string origin, string destination); 
    }
}
