using GameGenerator.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Repositories
{
    public interface IGameRepository
    {
        Task<List<GameEntry>> GetAllAsync();

        Task<GameEntry> GetByIdAsync(int id);

        Task<int> CreateAsync(GameEntry gameEntry);

        Task<int> UpdateAsync(int id, GameEntry updatedGameEntry);

        Task<int> DeleteAsync(int id);
        
    }
}
