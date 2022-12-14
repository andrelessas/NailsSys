using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NailsSys.API.Middleware;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Services;
using NailsSys.Infrastructure.Autenticacao;
using NailsSys.Infrastructure.Context;
using NailsSys.Infrastructure.Persistense;
using NailsSys.Infrastructure.Persistense.Repositories;

namespace NailsSys.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<NailsSysContext>(options => options.UseSqlServer(configuration.GetConnectionString("conexao")));
            // services.AddDbContext<NailsSysContext>(options => options.UseInMemoryDatabase("NailsBD"));
            
            services.AddMediatR(typeof(InserirProdutoCommand));
            services.AddScoped<GlobalExceptionHandlerMiddleware>();
            services.AddScoped<IProdutoRepository,ProdutoRepository>();
            services.AddScoped<IClienteRepository,ClienteRepository>();
            services.AddScoped<IItemAgendamentoRepository,ItemAgendamentoRepository>();
            services.AddScoped<IAgendamentoRepository,AgendamentoRepository>();
            services.AddScoped<IUsuarioRepository,UsuarioRepository>();
            services.AddScoped<IAutenticacaoService,AutenticacaoService>();
            services.AddScoped<IFormaPagamentoRepository,FormaPagamentoRepository>();
            services.AddScoped<IAtendimentoRepository,AtendimentoRepository>();
            services.AddScoped<IUnitOfWorks,UnitOfWorks>();
        }
    }
}