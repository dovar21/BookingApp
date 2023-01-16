namespace Booking.API.Application.AutoMapper;

using global::AutoMapper;
using Booking.API.Application.Models.ServiceObject;
using Booking.Domain.AggregatesModel.ServiceObjectAggregate;

public class BookingMappingProfile : Profile
{
    public BookingMappingProfile()
    {
        //Response
        CreateMap<ServiceObject, ServiceObjectResponseDTO>()
            .ForMember(m => m.Amount, opt => opt.MapFrom(src => src.Amount));
        //Response
        CreateMap<ServiceObject, ServiceObjectCreateUpdateResponseDTO>();
        CreateMap<ServiceObject, BookingResponseDTO>();


    }
}