using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Entities;

namespace NailsSys.Infrastructure.Context
{
    public class NailsSysContext:DbContext
    {
        public NailsSysContext(DbContextOptions options)
            :base(options)
        {}

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<ItemAgendamento> ItemAgendamento { get; set; }
        public DbSet<Atendimento> Atendimento { get; set; }
        public DbSet<ItemAtendimento> ItemAtendimento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}