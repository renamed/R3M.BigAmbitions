using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FakeItEasy;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation;

namespace BigAmbitions.Application.UnitTest;
public class BusinessApplicationUnitTest
{
    private readonly IBusinessesRepository _businessRepository;
    private readonly IValidator<Business> _businessValidator;
    private readonly BusinessesApplication _businessApplication;

    public BusinessApplicationUnitTest()
    {
        _businessRepository = A.Fake<IBusinessesRepository>();
        _businessValidator = A.Fake<IValidator<Business>>();

        _businessApplication = new(_businessRepository, _businessValidator);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenValidatorFails()
    {
        // Arrange
        var business = new Business();

        A.CallTo(() => _businessValidator.Validate(business))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Property", "Error Message") }));

        // Act
        var action = () => _businessApplication.AddAsync(business);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Error Message");

        A.CallTo(() => _businessRepository.AddAsync(business))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task AddAsync_ShouldReturnBusiness()
    {
        // Arrange
        var business = new Business();

        A.CallTo(() => _businessValidator.Validate(business))
            .Returns(new ValidationResult());

        // Act
        var newBusiness = await _businessApplication.AddAsync(business);

        // Assert
        newBusiness.Should().NotBeNull();
        newBusiness.Should().Be(business);

        A.CallTo(() => _businessRepository.AddAsync(business))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task FindAsync_ShouldReturnBusiness()
    {
        // Arrange
        var expectedBusiness = new Business { Id = 3 };

        A.CallTo(() => _businessRepository.FindAsync(expectedBusiness.Id))
            .Returns(expectedBusiness);

        // Act
        var business = await _businessApplication.FindAsync(expectedBusiness.Id);

        // Assert
        business.Should().NotBeNull();
        business.Should().Be(expectedBusiness);

        A.CallTo(() => _businessRepository.FindAsync(expectedBusiness.Id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task FindAsync_ShouldReturnNull_WhenBusinessNotFound()
    {
        // Arrange
        var businessId = 3;

        A.CallTo(() => _businessRepository.FindAsync(businessId))
            .Returns(null as Business);

        // Act
        var business = await _businessApplication.FindAsync(businessId);

        // Assert
        business.Should().BeNull();

        A.CallTo(() => _businessRepository.FindAsync(businessId))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task EditAsync_ShouldThrowException_WhenBusinessNotFound()
    {
        // Arrange
        var business = new Business();
        var businessId = 3;

        A.CallTo(() => _businessRepository.FindAsync(businessId))
            .Returns(null as Business);

        // Act
        var action = () => _businessApplication.EditAsync(businessId, business);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Business not found");

        A.CallTo(() => _businessRepository.UpdateAsync(A<Business>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task EditAsync_ShouldUpdateBusiness()
    {
        // Arrange
        var existingBusiness = new Business { Id = 3, Name = "OldName", DailyRent = 1000, Employees = new List<Employee>() };
        var updatedBusiness = new Business { Name = "NewName", DailyRent = 2000, Employees = new List<Employee>() };

        A.CallTo(() => _businessRepository.FindAsync(existingBusiness.Id))
            .Returns(existingBusiness);

        A.CallTo(() => _businessValidator.Validate(existingBusiness))
            .Returns(new ValidationResult());

        // Act
        var result = await _businessApplication.EditAsync(existingBusiness.Id, updatedBusiness);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(updatedBusiness.Name);
        result.DailyRent.Should().Be(updatedBusiness.DailyRent);
        result.Employees.Should().BeEquivalentTo(updatedBusiness.Employees);

        A.CallTo(() => _businessRepository.UpdateAsync(existingBusiness))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task EditAsync_ShouldThrowException_WhenValidatorFails()
    {
        // Arrange
        var existingBusiness = new Business { Id = 3, Name = "OldName", DailyRent = 1000, Employees = new List<Employee>() };
        var updatedBusiness = new Business { Name = "NewName", DailyRent = 2000, Employees = new List<Employee>() };

        A.CallTo(() => _businessRepository.FindAsync(existingBusiness.Id))
            .Returns(existingBusiness);

        A.CallTo(() => _businessValidator.Validate(existingBusiness))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Property", "Error Message") }));

        // Act
        var action = () => _businessApplication.EditAsync(existingBusiness.Id, updatedBusiness);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Error Message");

        A.CallTo(() => _businessRepository.UpdateAsync(existingBusiness))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task RemoveAsync_ShouldThrowException_WhenBusinessNotFound()
    {
        // Arrange
        var businessId = 3;

        A.CallTo(() => _businessRepository.FindAsync(businessId))
            .Returns(null as Business);

        // Act
        var action = () => _businessApplication.RemoveAsync(businessId);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Business not found");

        A.CallTo(() => _businessRepository.RemoveAsync(A<Business>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveBusiness()
    {
        // Arrange
        var business = new Business { Id = 3 };

        A.CallTo(() => _businessRepository.FindAsync(business.Id))
            .Returns(business);

        // Act
        await _businessApplication.RemoveAsync(business.Id);

        // Assert
        A.CallTo(() => _businessRepository.RemoveAsync(business))
            .MustHaveHappenedOnceExactly();
    }
}
