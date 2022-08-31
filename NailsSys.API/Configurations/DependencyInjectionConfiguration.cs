using MediatR;
using Microsoft.EntityFrameworkCore;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Core.Interfaces;
using NailsSys.Infrastructure.Context;
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
            services.AddScoped<IProdutoRepository,ProdutoRepository>();
            services.AddScoped<IClienteRepository,ClienteRepository>();
            services.AddScoped<IItemAgendamentoRepository,ItemAgendamentoRepository>();
            services.AddScoped<IAgendamentoRepository,AgendamentoRepository>();
        }
    }
}