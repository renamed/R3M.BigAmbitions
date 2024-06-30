namespace BigAmbitions.WebApi.Dtos;

public record GameRequestDto
(
    string Name
);

public record GameResponseDto
(
    int Id,
    string Name
);
