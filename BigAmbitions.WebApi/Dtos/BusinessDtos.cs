namespace BigAmbitions.WebApi.Dtos;

public record BusinessRequestDto
(
    string Name,
    decimal DailyRent,
    int GameId
);

public record BusinessResponseDto
(
    int Id,
    string Name,
    decimal DailyRent,
    int GameId
);
