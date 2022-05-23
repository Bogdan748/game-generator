using GameGenerator.Core.Abstractions.Repositories.MapUsers;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories.MapUsers
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<List<UserEntry>> GetAllAsync()
        {
            var userEntities = await _applicationDbContext
                .UserEntity.Include(u => u.Connections)
                .ToListAsync();

            return userEntities.Select(entity => entity.ToUserEntry())
                               .ToList();
        }

        public async Task<List<UserEntry>> GetAllByGroupAsync(string groupName)
        {
            var userEntities = await _applicationDbContext
                .UserEntity.Include(u => u.Connections)
                .Where(u=>u.UserGroup==groupName)
                .ToListAsync();

            return userEntities.Select(entity => entity.ToUserEntry())
                               .ToList();
        }

        public async Task<UserEntry> GetByIdAsync(string userName)
        {
            var userEntities = await _applicationDbContext
                .UserEntity.Include(u => u.Connections)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            return userEntities.ToUserEntry();
        }

        public async Task<int> CreateAsync(UserEntry userEntry)
        {
            var userEntity = new Entities.MapUsers.UserEntity()
            {
                UserName = userEntry.UserName,
                UserType = userEntry.UserType,
                UserGroup = userEntry.UserGroup,
                OnGoingGameEntity = _applicationDbContext.OnGoingGameEntity
                                                .FirstOrDefault(o=>o.GameGroup==userEntry.UserGroup)
            };

             _applicationDbContext.Add(userEntity);

            
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAsync(string userName)
        {
            var userEntity = await _applicationDbContext.UserEntity.FirstOrDefaultAsync(c => c.UserName==userName);

            if (userEntity is not null)
            {
                _applicationDbContext.Remove(userEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }
    }
}
