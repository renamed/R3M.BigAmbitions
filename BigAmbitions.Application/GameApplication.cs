using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FluentValidation;

namespace BigAmbitions.Application;

public class GameApplication :  IGameApplication
{
    private readonly IGameRepository _gameRepository;
    private readonly IValidator<Game> _validator;

    public GameApplication(IGameRepository gameRepository, IValidator<Game> validator)
    {
        _gameRepository = gameRepository;
        _validator = validator;
    }

    public async Task<Game> AddAsync(Game game)
    {
        ValidateAndThrow(game);

        await _gameRepository.AddAsync(game);
        return game;
    }

    public ValueTask<Game?> FindAsync(int id)
    {
        return _gameRepository.FindAsync(id);
    }

    public async Task<Game> EditAsync(int id, Game game)
    {
        var existingGame = await FindAsync(id)
            ?? throw new Exception("Game not found");

        existingGame.Name = game.Name;

        ValidateAndThrow(existingGame);

        await _gameRepository.UpdateAsync(existingGame);
        return existingGame;
    }

    public async Task RemoveAsync(int id)
    {
        var existingGame = await FindAsync(id)
            ?? throw new Exception("Game not found");

        await _gameRepository.RemoveAsync(existingGame);
    }

    private void ValidateAndThrow(Game game)
    {
        var validationResult = _validator.Validate(game);
        if (!validationResult.IsValid)
        {
            throw new Exception(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
