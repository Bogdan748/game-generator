using GameGenerator.Core.Abstractions.Repositories.MapUsers;
using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Models.MapUsers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Services.MapUsers
{
    internal class ConnectionService : IConnectionService
    {
        private readonly IConnectionRepository _repository;

        public ConnectionService(IConnectionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ConnectionEntry> GetConnection(string connectionId)
        {
            ConnectionEntry connectionEntries = await _repository.GetConnection(connectionId);
            return connectionEntries;
        }

        public  async Task<List<ConnectionEntry>> GetAllForUserAsync(string userName)
        {
            List<ConnectionEntry> connectionEntries = await _repository.GetAllForUserAsync(userName);
            return connectionEntries;
        }

        public async Task<int> CreateAsync(ConnectionEntry connectionEntry)
        {
            if (connectionEntry is null)
            {
                throw new ArgumentException(nameof(connectionEntry));
            }
            int affectedRows = await _repository.CreateAsync(connectionEntry);
            return affectedRows;
        }

        public async Task<int> DeleteAsync(string connectionID)
        {
            int affectedRows = await _repository.DeleteAsync(connectionID);
            return affectedRows;
        }

        

        public async Task<int> UpdateAsync(string connectionID, ConnectionEntry updatedConnectionEntry)
        {
            if (updatedConnectionEntry is null)
            {
                throw new ArgumentException(nameof(updatedConnectionEntry));
            }
            int affectedRows = await _repository.UpdateAsync(connectionID, updatedConnectionEntry);
            return affectedRows;
        }
    }
}
