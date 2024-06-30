using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using FluentValidation;

namespace BigAmbitions.Application.Validators;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
        RuleFor(x => x.Name)
            .DefaultForName();
    }
}
