using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using FluentValidation;

namespace BigAmbitions.Application.Validators;

public class WarehouseValidator : AbstractValidator<Warehouse>
{
    public WarehouseValidator()
    {
        RuleFor(x => x.Name).DefaultForName();
    }
}
