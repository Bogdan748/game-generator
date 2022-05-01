using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GameGenerator.Models;

namespace GameGenerator.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GameGenerator.Models.Game> Game { get; set; }
        public DbSet<GameGenerator.Models.Card> Card { get; set; }

        protected override void OnModelCreating(ModelBuilder moledBuilder)
        {
            base.OnModelCreating(moledBuilder);

            moledBuilder.Entity<Game>()
                .ToTable("GameEntries")
                .HasKey(cl => cl.Id);

            moledBuilder.Entity<Game>()
                .Property(cl => cl.Name)
                .IsRequired()
                .HasMaxLength(200);

            moledBuilder.Entity<Card>()
                .ToTable("CardEntries")
                .HasKey(cl => cl.Id);

            moledBuilder.Entity<Card>()
                .Property(cl => cl.Text)
                .HasMaxLength(500);

            moledBuilder.Entity<Card>()
                .HasOne(c => c.Game)
                .WithMany(g => g.Cards)
                .HasForeignKey(p=>p.GameId)
                .HasConstraintName("FK_Card_Game");
                
                
                
        }

    }
}
