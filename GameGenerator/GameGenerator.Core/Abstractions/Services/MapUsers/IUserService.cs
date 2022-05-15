using GameGenerator.Core.Models.MapUsers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Services.MapUsers
{
    public interface IUserService
    {
        Task<List<UserEntry>> GetAllAsync();

        Task<UserEntry> GetByIdAsync(string userName);

        Task<List<UserEntry>> GetAllByGroupAsync(string groupName);

        Task<int> CreateAsync(UserEntry userEntry);

        Task<int> DeleteAsync(string userName);
    }
}
