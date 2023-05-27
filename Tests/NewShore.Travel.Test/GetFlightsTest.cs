using Moq;
using NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination;
using NewShore.Travel.Domain.Features.Entities;
using NewShore.Travel.Domain.Features.Flights.Contracts.Integrations;

namespace NewShore.Travel.Test
{
    public class GetFlightsTest
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsJourneys()
        {
            // Arrange
            var origin = "MDE";
            var destination = "CTG";
            var maxNumberFlights = 3;

            var flights = new List<Flight>
            {
                new Flight { Origin = "MDE", Destination = "CTG", Price = 200 },
                new Flight { Origin = "MDE", Destination = "CTG", Price = 250 },
                new Flight { Origin = "MDE", Destination = "CTG", Price = 300 },
                new Flight { Origin = "MDE", Destination = "BOG", Price = 180 },
                new Flight { Origin = "BOG", Destination = "CTG", Price = 150 }
            };

            var flightsServiceMock = new Mock<IFlightsService>();
            flightsServiceMock.Setup(x => x.GetFlights()).ReturnsAsync(flights);

            var query = new GetFlightsByOriginAndDestinationQuery
            {
                Origin = origin,
                Destination = destination,
                MaxNumberFlights = maxNumberFlights
            };

            var handler = new GetFlightsByOriginAndDestinationQueryHandler(flightsServiceMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count);
            Assert.True(result.Any(j => j.Flights.Any(f => f.Origin == origin)));
            Assert.True(result.Any(j=> j.Flights.Any(f=> f.Destination == destination)));
            Assert.True(result.All(f => f.Flights.Count <= maxNumberFlights));
            foreach (var journey in result)
            {
                Assert.NotEmpty(journey.Flights);
            }
        }
    }
}