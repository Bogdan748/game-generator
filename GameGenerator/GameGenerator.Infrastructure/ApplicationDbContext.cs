﻿using GameGenerator.Infrastructure.Entities;
using GameGenerator.Infrastructure.Entities.MapUsers;
using GameGenerator.Infrastructure.Entities.OnGoingGame;
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

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<ConnectionEntity> ConnectionEntity { get; set; }

        public DbSet<OnGoingCardsEntity> OnGoingCardsEntity { get; set; }
        public DbSet<OnGoingGameEntity> OnGoingGameEntity { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }

    }
}
