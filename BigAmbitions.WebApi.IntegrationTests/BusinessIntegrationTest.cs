using BigAmbitions.WebApi.Dtos;
using BigAmbitions.WebApi.IntegrationTests.Fixtures;
using FluentAssertions;
using System.Net;

namespace BigAmbitions.WebApi.IntegrationTests;
public class BusinessIntegrationTest(CustomWebApplicationFactory<Program> applicationFactory)
    : IntegrationTestsBase(applicationFactory)
{
    private const string BaseBusinessUrl = "/api/Business";

    [Fact]
    public async Task AddAsync_ShouldReturnCreatedBusiness()
    {
        // Arrange
        var businessRequest = new BusinessRequestDto("New Business", 1000m, 1);

        // Act
        var response = await PostAsync<BusinessResponseDto, BusinessRequestDto>(BaseBusinessUrl, businessRequest);

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Body.Should().NotBeNull();
        response.Body.Name.Should().Be("New Business");
        response.Body.DailyRent.Should().Be(1000m);
        response.Body.GameId.Should().Be(1);
    }

    [Fact]
    public async Task FindAsync_ShouldReturnBusiness()
    {
        // Arrange
        var businessRequest = new BusinessRequestDto("New Business", 1000m, 1);
        var createResponse = await PostAsync<BusinessResponseDto, BusinessRequestDto>(BaseBusinessUrl, businessRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var businessId = createResponse.Body.Id;

        // Act
        var response = await GetAsync<BusinessResponseDto>($"{BaseBusinessUrl}/{businessId}");

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().NotBeNull();
        response.Body.Id.Should().Be(businessId);
    }

    [Fact]
    public async Task EditAsync_ShouldReturnUpdatedBusiness()
    {
        // Arrange
        var businessRequest = new BusinessRequestDto("New Business", 1000m, 1);
        var createResponse = await PostAsync<BusinessResponseDto, BusinessRequestDto>(BaseBusinessUrl, businessRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var businessId = createResponse.Body.Id;

        var updatedBusinessRequest = new BusinessRequestDto("Updated Business", 2000m, 1);

        // Act
        var response = await PutAsync<BusinessResponseDto, BusinessRequestDto>($"{BaseBusinessUrl}/{businessId}", updatedBusinessRequest);

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().NotBeNull();
        response.Body.Id.Should().Be(businessId);
        response.Body.Name.Should().Be("Updated Business");
        response.Body.DailyRent.Should().Be(2000m);
    }

    [Fact]
    public async Task RemoveAsync_ShouldReturnNoContent()
    {
        // Arrange
        var businessRequest = new BusinessRequestDto("New Business", 1000m, 1);
        var createResponse = await PostAsync<BusinessResponseDto, BusinessRequestDto>(BaseBusinessUrl, businessRequest);
        createResponse.Response.EnsureSuccessStatusCode();
        var businessId = createResponse.Body.Id;

        // Act
        var response = await DeleteAsync($"{BaseBusinessUrl}/{businessId}");

        // Assert
        response.Response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
