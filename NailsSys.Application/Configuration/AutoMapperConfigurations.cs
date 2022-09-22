using AutoMapper;
using NailsSys.Application.ViewModels;
using NailsSys.Core.DTOs;
using NailsSys.Core.Entities;
using NailsSys.Core.Models;

namespace NailsSys.Application.Configuration
{
    public class AutoMapperConfigurations : Profile
    {
        public AutoMapperConfigurations()
        {
            CreateMap<Agendamento, AgendamentoViewModel>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeCliente, conf => conf.MapFrom(src => src.Cliente.NomeCliente))
                .ForMember(dest => dest.Itens, conf => conf.MapFrom(src => src.ItemAgendamentos))
                .ReverseMap();

            CreateMap<ItemAgendamento, ItemAgendamentoViewModel>()
                .ForMember(dest => dest.NomeProduto, conf => conf.MapFrom(src => src.Produto.Descricao))
                .ReverseMap();

            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<PaginationResult<Cliente>, PaginationResult<ClienteViewModel>>()
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<PaginationResult<Produto>, PaginationResult<ProdutoViewModel>>()
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<PaginationResult<Usuario>, PaginationResult<UsuarioViewModel>>()
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<PaginationResult<ItemAgendamentoDTO>, PaginationResult<ItemAgendamentoViewModel>>()
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.Id, config => config.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<ItemAgendamento, ItemAgendamentoDTO>()
                .ForMember(dest => dest.Id, config => config.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<ItemAgendamentoDTO, ItemAgendamentoViewModel>()
                .ForMember(dest => dest.NomeProduto, conf => conf.MapFrom(src => src.DescricaoProduto))
                .ReverseMap();
        }
    }
}