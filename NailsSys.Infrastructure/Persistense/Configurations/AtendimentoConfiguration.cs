using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailsSys.Core.Entities;

namespace NailsSys.Infrastructure.Persistense.Configurations
{
    public class AtendimentoConfiguration : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(x => x.FormaPagamento) // propriedade do tipo do objeto
                    .WithMany(x=> x.Atendimentos) // lista de objetos
                    .HasForeignKey(x => x.IdFormaPagamento) // chave estrangeira 
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}