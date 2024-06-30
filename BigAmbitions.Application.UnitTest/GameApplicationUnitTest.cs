using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FakeItEasy;
using FluentAssertions;
using FluentValidation.Results;
using FluentValidation;

namespace BigAmbitions.Application.UnitTest;

public class GameApplicationUnitTest
{
    private readonly IGameRepository _gameRepository;
    private readonly IValidator<Game> _gameValidator;
    private readonly GameApplication _gameApplication;

    public GameApplicationUnitTest()
    {
        _gameRepository = A.Fake<IGameRepository>();
        _gameValidator = A.Fake<IValidator<Game>>();

        _gameApplication = new(_gameRepository, _gameValidator);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowException_WhenValidatorFails()
    {
        // Arrange
        var game = new Game();

        A.CallTo(() => _gameValidator.Validate(game))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Property", "Error Message") }));

        // Act
        var action = () => _gameApplication.AddAsync(game);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Error Message");

        A.CallTo(() => _gameRepository.AddAsync(game))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task AddAsync_ShouldReturnGame()
    {
        // Arrange
        var game = new Game();

        A.CallTo(() => _gameValidator.Validate(game))
            .Returns(new ValidationResult());

        // Act
        var newGame = await _gameApplication.AddAsync(game);

        // Assert
        newGame.Should().NotBeNull();
        newGame.Should().Be(game);

        A.CallTo(() => _gameRepository.AddAsync(game))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task FindAsync_ShouldReturnGame()
    {
        // Arrange
        var expectedGame = new Game { Id = 3 };

        A.CallTo(() => _gameRepository.FindAsync(expectedGame.Id))
            .Returns(expectedGame);

        // Act
        var game = await _gameApplication.FindAsync(expectedGame.Id);

        // Assert
        game.Should().NotBeNull();
        game.Should().Be(expectedGame);

        A.CallTo(() => _gameRepository.FindAsync(expectedGame.Id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task FindAsync_ShouldReturnNull_WhenGameNotFound()
    {
        // Arrange
        var gameId = 3;

        A.CallTo(() => _gameRepository.FindAsync(gameId))
            .Returns(null as Game);

        // Act
        var game = await _gameApplication.FindAsync(gameId);

        // Assert
        game.Should().BeNull();

        A.CallTo(() => _gameRepository.FindAsync(gameId))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task EditAsync_ShouldThrowException_WhenGameNotFound()
    {
        // Arrange
        var game = new Game();
        var gameId = 3;

        A.CallTo(() => _gameRepository.FindAsync(gameId))
            .Returns(null as Game);

        // Act
        var action = () => _gameApplication.EditAsync(gameId, game);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Game not found");

        A.CallTo(() => _gameRepository.UpdateAsync(A<Game>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task EditAsync_ShouldUpdateGame()
    {
        // Arrange
        var existingGame = new Game { Id = 3, Name = "OldName", Businesses = new List<Business>() };
        var updatedGame = new Game { Name = "NewName", Businesses = new List<Business>() };

        A.CallTo(() => _gameRepository.FindAsync(existingGame.Id))
            .Returns(existingGame);

        A.CallTo(() => _gameValidator.Validate(existingGame))
            .Returns(new ValidationResult());

        // Act
        var result = await _gameApplication.EditAsync(existingGame.Id, updatedGame);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(updatedGame.Name);
        result.Businesses.Should().BeEquivalentTo(updatedGame.Businesses);

        A.CallTo(() => _gameRepository.UpdateAsync(existingGame))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task EditAsync_ShouldThrowException_WhenValidatorFails()
    {
        // Arrange
        var existingGame = new Game { Id = 3, Name = "OldName", Businesses = new List<Business>() };
        var updatedGame = new Game { Name = "NewName", Businesses = new List<Business>() };

        A.CallTo(() => _gameRepository.FindAsync(existingGame.Id))
            .Returns(existingGame);

        A.CallTo(() => _gameValidator.Validate(existingGame))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Property", "Error Message") }));

        // Act
        var action = () => _gameApplication.EditAsync(existingGame.Id, updatedGame);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Error Message");

        A.CallTo(() => _gameRepository.UpdateAsync(existingGame))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task RemoveAsync_ShouldThrowException_WhenGameNotFound()
    {
        // Arrange
        var gameId = 3;

        A.CallTo(() => _gameRepository.FindAsync(gameId))
            .Returns(null as Game);

        // Act
        var action = () => _gameApplication.RemoveAsync(gameId);

        // Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Game not found");

        A.CallTo(() => _gameRepository.RemoveAsync(A<Game>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task RemoveAsync_ShouldRemoveGame()
    {
        // Arrange
        var game = new Game { Id = 3 };

        A.CallTo(() => _gameRepository.FindAsync(game.Id))
            .Returns(game);

        // Act
        await _gameApplication.RemoveAsync(game.Id);

        // Assert
        A.CallTo(() => _gameRepository.RemoveAsync(game))
            .MustHaveHappenedOnceExactly();
    }
}
