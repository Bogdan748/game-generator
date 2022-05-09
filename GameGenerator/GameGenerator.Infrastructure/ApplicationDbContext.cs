using GameGenerator.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameGenerator.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GameEntity> GameEntity { get; set; }
        public DbSet<CardEntity> CardEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder moledBuilder)
        {
            base.OnModelCreating(moledBuilder);

            moledBuilder.Entity<GameEntity>()
                .ToTable("GameEntries")
                .HasKey(cl => cl.Id);

            moledBuilder.Entity<GameEntity>()
                .Property(cl => cl.Name)
                .IsRequired()
                .HasMaxLength(200);

            moledBuilder.Entity<CardEntity>()
                .ToTable("CardEntries")
                .HasKey(cl => cl.Id);

            moledBuilder.Entity<CardEntity>()
                .Property(cl => cl.Text)
                .HasMaxLength(500);

            moledBuilder.Entity<CardEntity>()
                .HasOne(c => c.Game)
                .WithMany(g => g.Cards)
                .HasForeignKey(p => p.GameId)
                .HasConstraintName("FK_Card_Game");


        }

    }
}
