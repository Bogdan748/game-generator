using GameGenerator.Infrastructure.Entities;
using GameGenerator.Infrastructure.Entities.OnGoingGame;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class OnGoingCardsEntityConfiguration : IEntityTypeConfiguration<OnGoingCardsEntity>
    {
        public void Configure(EntityTypeBuilder<OnGoingCardsEntity> builder)
        {
            builder
                .ToTable("OnGoingCards")
                .HasKey(cl => cl.Id);


            builder
                .HasOne(c => c.User)
                .WithMany(u=>u.OnGoingCardsEntity)
                .HasForeignKey("UserId")
                .HasConstraintName("FK_OnGoing_User")
                .IsRequired();

            builder
                .HasOne(c => c.OnGoingGame)
                .WithMany(g=>g.OnGoingCards)
                .HasForeignKey("OnGoingGameId")
                .HasConstraintName("FK_OnGoing_Game")
                .IsRequired();
        }
    }
}
