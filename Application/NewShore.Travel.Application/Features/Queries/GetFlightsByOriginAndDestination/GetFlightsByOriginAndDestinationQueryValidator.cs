using FluentValidation;

namespace NewShore.Travel.Application.Features.Queries.GetFlightsByOriginAndDestination
{
    public class GetFlightsByOriginAndDestinationQueryValidator : AbstractValidator<GetFlightsByOriginAndDestinationQuery>
    {
        public GetFlightsByOriginAndDestinationQueryValidator()
        {
            RuleFor(f => f.Origin)
                .Length(3)
                .NotEqual(f => f.Destination);

            RuleFor(f => f.Destination)
                .Length(3);

            RuleFor(f => f.MaxNumberFlights)
                .InclusiveBetween(1, 5);
        }
    }
}
