namespace BigAmbitions.WebApi.Dtos;

public record EmployeeRequestDto(decimal Salary, int DailyHoursWork, int WeeklyDaysWork, int BusinessId);
public record EmployeeResponseDto(int Id, decimal Salary, int DailyHoursWork, int WeeklyDaysWork, int BusinessId);
