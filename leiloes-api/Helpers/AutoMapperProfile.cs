using AutoMapper;
using System;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<LeilaoDto, Leilao>()
                .ForMember(dest => dest.Abertura,
                           opts => opts.MapFrom(src => src.Abertura.Ticks))
                .ForMember(dest => dest.Finalizacao,
                           opts => opts.MapFrom(src => src.Finalizacao.Ticks))
                .ForMember(dest => dest.IndicUsado,
                           opts => opts.MapFrom(src => src.IndicUsado ? 1 : 0));

            CreateMap<Leilao, LeilaoDto>()
                .ForMember(dest => dest.Abertura,
                           opts => opts.MapFrom(src => new DateTime(src.Abertura)))
                .ForMember(dest => dest.Finalizacao,
                           opts => opts.MapFrom(src => new DateTime(src.Finalizacao)))
                .ForMember(dest => dest.IndicUsado,
                           opts => opts.MapFrom(src => src.IndicUsado == 1 ? true : false));
        }
    }
}