using BigAmbitions.Domain;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

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

    public static Response<T> Validate<T>(this T obj, IValidator<T> validator)
    {
        var response = new Response<T>
        {
            Data = obj
        };

        if (obj is null)
        {
            response.Message.Add("Parameter is invalid");
            return response;
        }

        return response;
    }

    public static Response<T> ValidateFull<T>(this T obj, IValidator<T> validator)
    {
        var response = Validate(obj, validator);

        var validationResult = validator.Validate(obj);
        if (!validationResult.IsValid)
        {
            response.Message.AddRange(validationResult.Errors.Select(s => s.ErrorMessage));
        }

        return response;
    }
}