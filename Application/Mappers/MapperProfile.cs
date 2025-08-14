using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ReservaCreateDto, Reserva>()
                .ForMember(dest => dest.Salon, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.Date));

            CreateMap<Reserva, ReservaDto>()
                .ForMember(dest => dest.Salon, opt => opt.MapFrom(src => src.Salon))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.Date));

            CreateMap<Salon, SalonDto>();
            CreateMap<Cliente, ClienteDto>();
        }
    }
}
