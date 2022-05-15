using GameGenerator.Infrastructure.Entities.MapUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameGenerator.Infrastructure.EntityConfigurations
{
    class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            
            builder
                .Property(u => u.UserName)
                .HasMaxLength(100);

            builder
                .Property(u => u.UserType)
                .HasMaxLength(100);

            builder
                .Property(u => u.UserGroup)
                .HasMaxLength(100);

            builder
                  .HasKey(cl => cl.UserName);
        }
    }
}
