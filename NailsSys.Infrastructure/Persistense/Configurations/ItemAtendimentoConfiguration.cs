using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NailsSys.Core.Entities;

namespace NailsSys.Infrastructure.Persistense.Configurations
{
    public class ItemAtendimentoConfiguration:IEntityTypeConfiguration<ItemAtendimento>
    {
        public void Configure(EntityTypeBuilder<ItemAtendimento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(i => i.Produto)
                    .WithMany(i => i.ItemAtendimentos)
                    .HasForeignKey(i => i.IdProduto)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Atendimento)
                    .WithMany(i => i.ItensAtendimento)
                    .HasForeignKey(i => i.IdAtendimento)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}