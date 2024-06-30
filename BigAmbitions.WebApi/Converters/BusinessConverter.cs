using AutoMapper;
using BigAmbitions.Domain;
using BigAmbitions.WebApi.Dtos;

namespace BigAmbitions.WebApi.Converters;

public class BusinessConverter : Profile
{
    public BusinessConverter()
    {
        CreateMap<BusinessRequestDto, Business>();
        CreateMap<Business, BusinessResponseDto>();
    }
}
