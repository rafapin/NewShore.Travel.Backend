using NewShore.Travel.Domain.Features.Entities;
using NewShore.Travel.Domain.Features.Flights.Contracts.Integrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewShore.Travel.Infrastructure.Integrations.Flights.NewShore
{
    public class NewShoreFlightService : IFlightsService
    {

        private readonly HttpClient _httpClient;
        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public NewShoreFlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var urlNewshoreService = Environment.GetEnvironmentVariable("newShoreUrlApi") ?? throw new NullReferenceException("newShoreUrlApi");
            _httpClient.BaseAddress = new Uri(urlNewshoreService); 
        }

        public async Task<List<Flight>> GetFlights()
        {
            if (NewShoreFlightStore.Flights == null)
            {
                var response = await _httpClient.GetAsync($"flights/2");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                NewShoreFlightStore.Flights = JsonSerializer.Deserialize<List<NewShoreFlight>>(responseString, defaultJsonSerializerOptions);
            }
            return NewShoreFlightStore.Flights
                    .Select(flight=> new Flight
                    {
                        Origin = flight.DepartureStation,
                        Destination = flight.ArrivalStation,
                        Price = flight.Price,
                        Transport =new Transport
                        {
                            FlightCarries = flight.FlightCarrier,
                            FlightNumber = flight.FlightNumber
                        }
                    })
                    .ToList();
        }
    }
}
