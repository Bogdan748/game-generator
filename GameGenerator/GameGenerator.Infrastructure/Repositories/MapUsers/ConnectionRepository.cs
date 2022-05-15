using GameGenerator.Core.Abstractions.Repositories.MapUsers;
using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Infrastructure.Entities.MapUsers;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories.MapUsers
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ConnectionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ConnectionEntry> GetConnection(string connectionId)
        {
            var connectionEntitie = await _applicationDbContext.ConnectionEntity
                .Include(c=>c.UserEntity)
                .FirstOrDefaultAsync(c => c.ConnectionID == connectionId);
                

            return connectionEntitie.ToConnectionEntry();
        }

        public async Task<List<ConnectionEntry>> GetAllForUserAsync(string userName)
        {
            var connectionEntitie = await _applicationDbContext.ConnectionEntity
                .Include(c => c.UserEntity)
                .Where(c => c.UserEntity.UserName == userName)
                .ToListAsync();

            return connectionEntitie.Select(c => c.ToConnectionEntry()).ToList();
        }

        public async Task<int> CreateAsync(ConnectionEntry connectionEntry)
        {
            UserEntity userEntity = await _applicationDbContext.UserEntity
                .FirstOrDefaultAsync(u => u.UserName == connectionEntry.UserName);

            if(userEntity is null)
            {
                throw new ArgumentNullException(nameof(userEntity));
            }

            var connectionEntity = new ConnectionEntity()
            {
                ConnectionID=connectionEntry.ConnectionID,
                UserAgent=connectionEntry.UserAgent,
                Connected=connectionEntry.Connected,
                UserEntity=userEntity
            };

            _applicationDbContext.Add(connectionEntity);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> UpdateAsync(string connectionID, ConnectionEntry updatedConnectionEntry)
        {
            var connectionEntity = await _applicationDbContext.ConnectionEntity.FirstOrDefaultAsync(c => c.ConnectionID == connectionID);

            if (connectionEntity is not null)
            {
                UserEntity userEntity = await _applicationDbContext.UserEntity
                    .FirstOrDefaultAsync(u => u.UserName == updatedConnectionEntry.UserName);

                
                connectionEntity.UserAgent = updatedConnectionEntry.UserAgent;
                connectionEntity.Connected = updatedConnectionEntry.Connected;
                connectionEntity.UserEntity = userEntity;

                try
                {
                    _applicationDbContext.Update(connectionEntity);
                    int affectedRows = await _applicationDbContext.SaveChangesAsync();

                    return affectedRows;
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    connectionEntity = await _applicationDbContext.ConnectionEntity.FirstOrDefaultAsync(c => c.ConnectionID == connectionID);

                    if(connectionEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating Connection entry", ex);
                    }

                }


            }
            return 0;

        }

        public async Task<int> DeleteAsync(string connectionID)
        {
            var connectionEntity = await _applicationDbContext.ConnectionEntity.FirstOrDefaultAsync(c => c.ConnectionID == connectionID);

            if (connectionEntity is not null)
            {
                _applicationDbContext.Remove(connectionEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }


        
    }
}
