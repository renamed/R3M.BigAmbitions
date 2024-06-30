using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;

namespace BigAmbitions.WebApi.Converters;

public class EmployeeConverter : Profile
{
    public EmployeeConverter()
    {
        CreateMap<EmployeeRequestDto, Employee>();
        CreateMap<Employee, EmployeeResponseDto>();
    }
}
