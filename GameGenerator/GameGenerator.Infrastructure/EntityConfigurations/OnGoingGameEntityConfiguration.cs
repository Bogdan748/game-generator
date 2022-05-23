using GameGenerator.Infrastructure.Entities.OnGoingGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class OnGoingGameEntityConfiguration : IEntityTypeConfiguration<OnGoingGameEntity>
    {
        public void Configure(EntityTypeBuilder<OnGoingGameEntity> builder)
        {
            builder
                .ToTable("OnGoingGames")
                .HasKey(g => g.Id);

            builder
                .Property(cl => cl.GameGroup)
                .HasMaxLength(200);

            builder
                .HasOne(c => c.Game)
                .WithMany(g => g.OnGoingGames)
                .HasForeignKey("GameId")
                .HasConstraintName("FK_Card_Game")
                .IsRequired();
        }
    }
}
