using FluentValidation;

namespace BigAmbitions.Application.Extensions;

public static class DefaultValidatorsExtensions
{
    public static IRuleBuilderOptions<T, string> DefaultForName<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .WithMessage("{propertyName} is required")
                .MinimumLength(3)
                .WithMessage("{propertyName} must be at least 3 chars length")
                .MaximumLength(50)
                .WithMessage("{propertyName} must not be over 50 chars length");
    }

    public static IRuleBuilderOptions<T, int> DefaultForPositiveInt<T>(this IRuleBuilderInitial<T, int> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .WithMessage("{propertyName} is required")
                .GreaterThan(0)
                .WithMessage("{propertyName} should be greater than zero");
    }

    public static IRuleBuilderOptions<T, decimal> DefaultForPositiveDecimal<T>(this IRuleBuilderInitial<T, decimal> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .WithMessage("{propertyName} is required")
                .GreaterThan(0)
                .WithMessage("{propertyName} should be greater than zero");
    }
}