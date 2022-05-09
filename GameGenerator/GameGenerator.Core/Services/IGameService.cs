using GameGenerator.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Services
{
    public interface IGameService
    {
        Task<List<GameEntry>> GetAllAsync();

        Task<GameEntry> GetByIdAsync(int id);

        Task<bool> CreateAsync(GameEntry gameEntry);

        Task<bool> UpdateAsync(int id, GameEntry updatedGameEntry);

        Task<bool> DeleteAsync(int id);
    }
}
