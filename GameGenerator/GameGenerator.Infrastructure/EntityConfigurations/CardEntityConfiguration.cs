using GameGenerator.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
                .HasForeignKey("GameId")
                .HasConstraintName("FK_Card_Game")
                .IsRequired();
        }
    }
}
