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
                .ForMember(dest => dest.ValorBruto, conf => conf.MapFrom(src => src.PrecoInicial))
                .ForMember(dest => dest.ValorLiquido, conf => conf.MapFrom(src => src.PrecoInicial))
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

            // CreateMap<ItemAtendimento, ItemAgendamentoViewModel>()
            //     .ForMember(dest => dest.Item, conf => conf.MapFrom(src => src.Item))
            //     .ForMember(dest => dest.NomeProduto, conf => conf.MapFrom(src => src.Produto.Descricao))
            //     .ReverseMap();
            
            CreateMap<FormaPagamento, FormaPagamentoViewModel>()
                .ForMember(dest => dest.Id, conf => conf.MapFrom(src => src.Id))
                .ReverseMap();
            
            CreateMap<PaginationResult<FormaPagamento>, PaginationResult<FormaPagamentoViewModel>>()
                .ForMember(dest => dest.Data, conf => conf.MapFrom(src => src.Data))
                .ReverseMap();
           
            CreateMap<ItemAtendimento,ItemAtendimentoViewModel>()
                .ForMember(dest => dest.Item, conf => conf.MapFrom(src => src.Item))
                .ForMember(dest => dest.NomeProduto, conf => conf.MapFrom(src => src.Produto.Descricao))
                .ForMember(dest => dest.Quantidade, conf => conf.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.ValorBruto, conf => conf.MapFrom(src => src.ValorBruto))
                .ForMember(dest => dest.ValorLiquido, conf => conf.MapFrom(src => src.ValorLiquido))
                .ReverseMap();

            CreateMap<Atendimento,AtendimentoViewModel>()
                .ForMember(dest=> dest.IdAtendimento,conf=> conf.MapFrom(src => src.Id))
                .ForMember(dest=> dest.NomeCliente,conf=> conf.MapFrom(src => src.Cliente.NomeCliente))
                .ForMember(dest=> dest.DescricaoFormaPagamento,conf=> conf.MapFrom(src => src.FormaPagamento.Descricao))
                .ForMember(dest=> dest.Itens,conf=> conf.MapFrom(src => src.ItensAtendimento))
                .ForMember(dest=> dest.StatusAtendimento,conf=> conf.MapFrom(src => src.Status))
                .ForMember(dest=> dest.DataAtendimento,conf=> conf.MapFrom(src => src.DataAtendimento))
                .ForMember(dest=> dest.ValorBruto,conf=> conf.MapFrom(src => src.ValorBruto))
                .ForMember(dest=> dest.ValorTotal,conf=> conf.MapFrom(src => src.ValorLiquido))
                .ForMember(dest=> dest.Desconto,conf=> conf.MapFrom(src => src.Desconto))
                .ReverseMap();
        }
    }
}