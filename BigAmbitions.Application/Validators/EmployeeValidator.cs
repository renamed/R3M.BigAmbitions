using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using FluentValidation;

namespace BigAmbitions.Application.Validators;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Salary)
            .DefaultForPositiveDecimal();

        RuleFor(x => x.DailyHoursWork)
            .DefaultForPositiveInt();

        RuleFor(x => x.WeeklyDaysWork)
            .DefaultForPositiveInt();
    }
}
