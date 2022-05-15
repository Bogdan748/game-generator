using GameGenerator.Infrastructure.Entities.MapUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class ConnectionEntityConfiguration : IEntityTypeConfiguration<ConnectionEntity>
    {
        public void Configure(EntityTypeBuilder<ConnectionEntity> builder)
        {
            builder
                  .HasKey(c => c.ConnectionID);

            builder
               .Property(c => c.UserAgent)
               .HasMaxLength(200);

            builder
               .Property(c => c.ConnectionID)
               .HasMaxLength(200);

            builder
                .HasOne(u => u.UserEntity)
                .WithMany(c => c.Connections)
                .HasConstraintName("FK_Connection_User")
                .IsRequired();
        }
    }
}
