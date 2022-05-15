﻿using GameGenerator.Core.Models.MapUsers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Services.MapUsers
{
    public interface IConnectionService
    {
        Task<List<ConnectionEntry>> GetAllForUserAsync(string userName);

        Task<ConnectionEntry> GetConnection(string connectionId);

        Task<int> CreateAsync(ConnectionEntry connectionEntry);

        Task<int> UpdateAsync(string connectionID, ConnectionEntry updatedConnectionEntry);

        Task<int> DeleteAsync(string connectionID);
    }
}
