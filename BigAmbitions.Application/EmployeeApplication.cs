using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FluentValidation;

namespace BigAmbitions.Application;

public class EmployeeApplication : IEmployeeApplication
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<Employee> _validator;

    public EmployeeApplication(IEmployeeRepository employeeRepository, IValidator<Employee> validator)
    {
        _employeeRepository = employeeRepository;
        _validator = validator;
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        ValidateAndThrow(employee);

        await _employeeRepository.AddAsync(employee);
        return employee;
    }

    public ValueTask<Employee?> FindAsync(int id)
    {
        return _employeeRepository.FindAsync(id);
    }

    public async Task<Employee> EditAsync(int id, Employee employee)
    {
        var existingEmployee = await FindAsync(id)
            ?? throw new Exception();

        existingEmployee.Salary = employee.Salary;
        existingEmployee.DailyHoursWork = employee.DailyHoursWork;
        existingEmployee.WeeklyDaysWork = employee.WeeklyDaysWork;

        ValidateAndThrow(existingEmployee);

        await _employeeRepository.UpdateAsync(existingEmployee);
        return existingEmployee;
    }

    public async Task RemoveAsync(int id)
    {
        var existingEmployee = await FindAsync(id)
            ?? throw new Exception();

        await _employeeRepository.RemoveAsync(existingEmployee);
    }

    private void ValidateAndThrow(Employee employee)
    {
        var validationResult = _validator.Validate(employee);
        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
