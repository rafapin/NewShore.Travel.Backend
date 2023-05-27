using System.Collections.Generic;

namespace NewShore.Travel.Domain.Features.Entities
{
    public class Journey
    {
        public Journey(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
            Flights = new List<Flight>();
        }

        public List<Flight> Flights { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }

    }
}
