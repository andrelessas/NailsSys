using AutoMapper;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;

namespace NailsSys.Application.Configuration
{
    public class AutoMapperConfigurations:Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<Agendamento,AgendamentoViewModel>()
                .ForMember(dest => dest.Id,conf => conf.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeCliente, conf => conf.MapFrom(src => src.Cliente.NomeCliente))
                .ForMember(dest => dest.Itens, conf => conf.MapFrom(src => src.ItemAgendamentos))
                .ReverseMap();
                
            CreateMap<ItemAgendamento,ItemAgendamentoViewModel>()
                .ForMember(dest => dest.NomeProduto, conf => conf.MapFrom(src => src.Produto.Descricao))
                .ReverseMap();

            CreateMap<Cliente,ClienteViewModel>()
                .ForMember(dest => dest.Id,conf => conf.MapFrom(src=>src.Id))
                .ReverseMap();
        }
    }
}