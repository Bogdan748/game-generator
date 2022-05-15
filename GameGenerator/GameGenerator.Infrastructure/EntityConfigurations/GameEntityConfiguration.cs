using GameGenerator.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class GameEntityConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder
                .ToTable("GameEntries")
                .HasKey(cl => cl.Id);

            builder
                .Property(cl => cl.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
