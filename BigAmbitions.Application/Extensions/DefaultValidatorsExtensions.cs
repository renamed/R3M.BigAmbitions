using FluentValidation;

namespace BigAmbitions.Application.Extensions;

public static class DefaultValidatorsExtensions
{
    public static IRuleBuilderOptions<T, string> DefaultForName<T>(this IRuleBuilderInitial<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);
    }
}