using GameGenerator.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class CardEntityConfiguration : IEntityTypeConfiguration<CardEntity>
    {
        public void Configure(EntityTypeBuilder<CardEntity> builder)
        {
            builder
                .ToTable("CardEntries")
                .HasKey(cl => cl.Id);

            builder
                .Property(cl => cl.Text)
                .HasMaxLength(500);

            builder
                .HasOne(c => c.Game)
                .WithMany(g => g.Cards)
                .HasForeignKey(p => p.GameId)
                .HasConstraintName("FK_Card_Game");
        }
    }
}
