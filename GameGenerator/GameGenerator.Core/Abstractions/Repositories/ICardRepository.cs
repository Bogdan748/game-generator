using GameGenerator.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Repositories
{
    public interface ICardRepository
    {
        Task<List<CardEntry>> GetAllAsync();

        Task<CardEntry> GetByIdAsync(int id);

        Task<int> CreateAsync(CardEntry cardEntry);

        Task<int> UpdateAsync(int id, CardEntry updatedCardEntry);

        Task<int> DeleteAsync(int id);

        Task<List<CardEntry>> GetAllAvailableForGameAsync(int gameId);

    }
}
