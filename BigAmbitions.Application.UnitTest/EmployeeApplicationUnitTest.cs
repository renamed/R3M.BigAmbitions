using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;

namespace BigAmbitions.Application.UnitTest;

public class EmployeeApplicationUnitTest
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<Employee> _employeeValidator;
    private readonly EmployeeApplication _employeeApplication;

    public EmployeeApplicationUnitTest()
    {
        _employeeRepository = A.Fake<IEmployeeRepository>();
        _employeeValidator = A.Fake<IValidator<Employee>>();

        _employeeApplication = new(_employeeRepository, _employeeValidator);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenValidatorFails()
    {
        // Arrange
        var employee = new Employee();

        A.CallTo(() => _employeeValidator.Validate(employee))
            .Returns(new ValidationResult(new[] { new ValidationFailure() }));

        // Act
        var action = () => _employeeApplication.AddAsync(employee);

        // Assert
        await action.Should().ThrowAsync<Exception>();

        A.CallTo(() => _employeeRepository.AddAsync(employee))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task AddAsync_ShouldReturnEmployee()
    {
        // Arrange
        var employee = new Employee();

        A.CallTo(() => _employeeValidator.Validate(employee))
            .Returns(new ValidationResult());

        // Act
        var newEmployee = await _employeeApplication.AddAsync(employee);

        // Assert
        newEmployee.Should().NotBeNull();
        newEmployee.Should().Be(employee);

        A.CallTo(() => _employeeRepository.AddAsync(employee))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task FindAsync_ShouldReturn()
    {
        // Arrange
        var expectedEmployee = new Employee();
        var employeeId = 3;

        expectedEmployee.Id = employeeId;

        A.CallTo(() => _employeeRepository.FindAsync(employeeId))
            .Returns(expectedEmployee);

        // Act
        var employee = await _employeeApplication.FindAsync(employeeId);

        // Assert
        employee.Should().NotBeNull();
        employee.Should().Be(expectedEmployee);

        A.CallTo(() => _employeeRepository.FindAsync(employeeId))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task EditAsync_ShouldThrowException_WhenEmployeeNotFound()
    {
        // Arrange
        var employee = new Employee();
        var employeeId = 3;

        A.CallTo(() => _employeeRepository.FindAsync(employeeId))
            .Returns(null as Employee);

        // Act
        var action = () => _employeeApplication.EditAsync(employeeId, employee);

        // Assert
        await action.Should().ThrowAsync<Exception>();

        A.CallTo(() => _employeeRepository.UpdateAsync(A<Employee>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task EditAsync_ShouldUpdateEmployee()
    {
        // Arrange
        var existingEmployee = new Employee { Id = 3, Salary = 1000, DailyHoursWork = 8, WeeklyDaysWork = 5 };
        var updatedEmployee = new Employee { Salary = 2000, DailyHoursWork = 9, WeeklyDaysWork = 4 };

        A.CallTo(() => _employeeRepository.FindAsync(existingEmployee.Id))
            .Returns(existingEmployee);

        A.CallTo(() => _employeeValidator.Validate(existingEmployee))
            .Returns(new ValidationResult());

        // Act
        var result = await _employeeApplication.EditAsync(existingEmployee.Id, updatedEmployee);

        // Assert
        result.Should().NotBeNull();
        result.Salary.Should().Be(updatedEmployee.Salary);
        result.DailyHoursWork.Should().Be(updatedEmployee.DailyHoursWork);
        result.WeeklyDaysWork.Should().Be(updatedEmployee.WeeklyDaysWork);

        A.CallTo(() => _employeeRepository.UpdateAsync(existingEmployee))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task RemoveAsync_ShouldThrowException_WhenEmployeeNotFound()
    {
        // Arrange
        var employeeId = 3;

        A.CallTo(() => _employeeRepository.FindAsync(employeeId))
            .Returns(null as Employee);

        // Act
        var action = () => _employeeApplication.RemoveAsync(employeeId);

        // Assert
        await action.Should().ThrowAsync<Exception>();

        A.CallTo(() => _employeeRepository.RemoveAsync(A<Employee>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveEmployee()
    {
        // Arrange
        var employee = new Employee { Id = 3 };

        A.CallTo(() => _employeeRepository.FindAsync(employee.Id))
            .Returns(employee);

        // Act
        await _employeeApplication.RemoveAsync(employee.Id);

        // Assert
        A.CallTo(() => _employeeRepository.RemoveAsync(employee))
            .MustHaveHappenedOnceExactly();
    }
}

