﻿namespace NewShore.Travel.Domain.Features.Entities
{
    public class Flight
    {
        public Transport Transport { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }

    }
}