using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailsSys.Core.Entities;

namespace NailsSys.Infrastructure.Persistense.Configurations
{
    public class ItemAgendamentoConfiguration : IEntityTypeConfiguration<ItemAgendamento>
    {
        public void Configure(EntityTypeBuilder<ItemAgendamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(i => i.Produto)
                    .WithMany(i => i.ItemAgendamentos)
                    .HasForeignKey(i => i.IdProduto)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Agendamento)
                    .WithMany(i => i.ItemAgendamentos)
                    .HasForeignKey(i => i.IdAgendamento)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}