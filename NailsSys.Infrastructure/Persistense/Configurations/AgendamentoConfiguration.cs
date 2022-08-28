using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailsSys.Core.Entities;

namespace NailsSys.Infrastructure.Persistense.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.HasOne(a => a.Cliente) 
                    .WithMany(a => a.Agendamentos)
                    .HasForeignKey(a => a.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}