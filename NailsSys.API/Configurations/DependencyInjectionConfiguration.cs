using MediatR;
using Microsoft.EntityFrameworkCore;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Services.Implementations;
using NailsSys.Application.Services.Interfaces;
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
            services.AddScoped<IItemAgendamentoService,ItemAgendamentoService>();
            services.AddScoped<IAgendamentoService,AgendamentoService>();
        }
    }
}