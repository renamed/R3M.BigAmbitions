using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;

namespace BigAmbitions.WebApi.Converters;

public class GameConverter : Profile
{
    public GameConverter()
    {
        CreateMap<GameRequestDto, Game>();
        CreateMap<Game, GameResponseDto>();
    }
}
