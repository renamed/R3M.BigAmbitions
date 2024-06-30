using BigAmbitions.WebApi.Dtos;
using BigAmbitions.WebApi.IntegrationTests.Fixtures;
using FluentAssertions;
using System.Net;

namespace BigAmbitions.WebApi.IntegrationTests;

public class EmployeeIntegrationTest(CustomWebApplicationFactory<Program> applicationFactory) 
    : IntegrationTestsBase(applicationFactory)
{
    private const string BaseEmployeeUrl = "/api/Employee";

    [Fact]
    public async Task AddAsync_ShouldReturnCreatedEmployee()
    {
        // Arrange
        var employeeRequest = new EmployeeRequestDto(3000, 8, 5, 1);

        // Act
        var response = await PostAsync<EmployeeResponseDto, EmployeeRequestDto>(BaseEmployeeUrl, employeeRequest);

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Body.Should().NotBeNull();
        response.Body.Salary.Should().Be(3000);
        response.Body.DailyHoursWork.Should().Be(8);
        response.Body.WeeklyDaysWork.Should().Be(5);
        response.Body.BusinessId.Should().Be(1);
    }

    [Fact]
    public async Task FindAsync_ShouldReturnEmployee()
    {
        // Arrange
        var employeeRequest = new EmployeeRequestDto(3000, 8, 5, 1);
        var createResponse = await PostAsync<EmployeeResponseDto, EmployeeRequestDto>(BaseEmployeeUrl, employeeRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var employeeId = createResponse.Body.Id;

        // Act
        var response = await GetAsync<EmployeeResponseDto>($"{BaseEmployeeUrl}/{employeeId}");

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().NotBeNull();
        response.Body.Id.Should().Be(employeeId);
    }

    [Fact]
    public async Task EditAsync_ShouldReturnUpdatedEmployee()
    {
        // Arrange
        var employeeRequest = new EmployeeRequestDto(3000, 8, 5, 1);
        var createResponse = await PostAsync<EmployeeResponseDto, EmployeeRequestDto>(BaseEmployeeUrl, employeeRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var employeeId = createResponse.Body.Id;

        var updatedEmployeeRequest = new EmployeeRequestDto(4000, 9, 6, 1);

        // Act
        var response = await PutAsync<EmployeeResponseDto, EmployeeRequestDto>($"{BaseEmployeeUrl}/{employeeId}", updatedEmployeeRequest);

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().NotBeNull();
        response.Body.Id.Should().Be(employeeId);
        response.Body.Salary.Should().Be(4000);
        response.Body.DailyHoursWork.Should().Be(9);
        response.Body.WeeklyDaysWork.Should().Be(6);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnNoContent()
    {
        // Arrange
        var employeeRequest = new EmployeeRequestDto(3000, 8, 5, 1);
        var createResponse = await PostAsync<EmployeeResponseDto, EmployeeRequestDto>(BaseEmployeeUrl, employeeRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var employeeId = createResponse.Body.Id;

        // Act
        var response = await DeleteAsync($"{BaseEmployeeUrl}/{employeeId}");

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}

