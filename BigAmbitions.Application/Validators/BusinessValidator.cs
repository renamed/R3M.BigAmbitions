using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using FluentValidation;

namespace BigAmbitions.Application.Validators;

public class BusinessValidator : AbstractValidator<Business>
{
    public BusinessValidator()
    {
        RuleFor(x => x.Name)
            .DefaultForName();

        RuleFor(x => x.DailyRent)
            .DefaultForPositiveDecimal();
    }
}
