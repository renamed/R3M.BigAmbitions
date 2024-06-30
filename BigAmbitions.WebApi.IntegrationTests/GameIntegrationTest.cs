using BigAmbitions.WebApi.Dtos;
using BigAmbitions.WebApi.IntegrationTests.Fixtures;
using FluentAssertions;
using System.Net;

namespace BigAmbitions.WebApi.IntegrationTests
{
    public class GameIntegrationTest(CustomWebApplicationFactory<Program> applicationFactory)
        : IntegrationTestsBase(applicationFactory)
    {
        private const string BaseGameUrl = "/api/Game";

        [Fact]
        public async Task AddAsync_ShouldReturnCreatedGame()
        {
            // Arrange
            var gameRequest = new GameRequestDto("New Game");

            // Act
            var response = await PostAsync<GameResponseDto, GameRequestDto>(BaseGameUrl, gameRequest);

            // Assert
            response.Response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Body.Should().NotBeNull();
            response.Body.Name.Should().Be("New Game");
            response.Body.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnGame()
        {
            // Arrange
            var gameRequest = new GameRequestDto("New Game");
            var createResponse = await PostAsync<GameResponseDto, GameRequestDto>(BaseGameUrl, gameRequest);
            createResponse.Response.EnsureSuccessStatusCode();
            var gameId = createResponse.Body.Id;

            // Act
            var response = await GetAsync<GameResponseDto>($"{BaseGameUrl}/{gameId}");

            // Assert
            response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Body.Should().NotBeNull();
            response.Body.Id.Should().Be(gameId);
        }

        [Fact]
        public async Task EditAsync_ShouldReturnUpdatedGame()
        {
            // Arrange
            var gameRequest = new GameRequestDto("New Game");
            var createResponse = await PostAsync<GameResponseDto, GameRequestDto>(BaseGameUrl, gameRequest);
            createResponse.Response.EnsureSuccessStatusCode();
            var gameId = createResponse.Body.Id;

            var updatedGameRequest = new GameRequestDto("Updated Game");

            // Act
            var response = await PutAsync<GameResponseDto, GameRequestDto>($"{BaseGameUrl}/{gameId}", updatedGameRequest);

            // Assert
            response.Response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Body.Should().NotBeNull();
            response.Body.Id.Should().Be(gameId);
            response.Body.Name.Should().Be("Updated Game");
        }

        [Fact]
        public async Task RemoveAsync_ShouldReturnNoContent()
        {
            // Arrange
            var gameRequest = new GameRequestDto("New Game");
            var createResponse = await PostAsync<GameResponseDto, GameRequestDto>(BaseGameUrl, gameRequest);
            createResponse.Response.EnsureSuccessStatusCode();
            var gameId = createResponse.Body.Id;

            // Act
            var response = await DeleteAsync($"{BaseGameUrl}/{gameId}");

            // Assert
            response.Response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Context.Games.Find(gameId).Should().BeNull();
        }
    }
}
