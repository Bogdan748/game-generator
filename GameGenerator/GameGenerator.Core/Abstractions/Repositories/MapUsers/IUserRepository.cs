using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Core.Models.OnGoingGame;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Repositories.MapUsers
{
    public interface IUserRepository
    {
        Task<List<UserEntry>> GetAllAsync();

        Task<UserEntry> GetByIdAsync(string userName);

        Task<List<UserEntry>> GetAllByGroupAsync(string groupName);

        Task<int> CreateAsync(UserEntry userEntry);

        Task<int> DeleteAsync(string userName);

        Task<int> UpdateAsync(string userName, UserEntry updatedUserEntry);
    }
}
