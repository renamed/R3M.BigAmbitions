using AutoMapper;
using BigAmbitions.Application.Contracts;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigAmbitions.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameApplication _gameApplication;
    private readonly IMapper _mapper;

    public GameController(IGameApplication gameApplication, IMapper mapper)
    {
        _gameApplication = gameApplication;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<GameResponseDto>> AddAsync([FromBody] GameRequestDto gameRequestDto)
    {
        var game = _mapper.Map<Game>(gameRequestDto);
        var addedGame = await _gameApplication.AddAsync(game);
        var gameResponseDto = _mapper.Map<GameResponseDto>(addedGame);
        return Created(Request.Path, gameResponseDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameResponseDto>> FindAsync(int id)
    {
        var game = await _gameApplication.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        var gameResponseDto = _mapper.Map<GameResponseDto>(game);
        return Ok(gameResponseDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GameResponseDto>> EditAsync(int id, [FromBody] GameRequestDto gameRequestDto)
    {
        var game = _mapper.Map<Game>(gameRequestDto);
        var updatedGame = await _gameApplication.EditAsync(id, game);
        var gameResponseDto = _mapper.Map<GameResponseDto>(updatedGame);
        return Ok(gameResponseDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        await _gameApplication.RemoveAsync(id);
        return NoContent();
    }
}
