using GameGenerator.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Services
{
    public interface ICardService
    {
        Task<List<CardEntry>> GetAllAsync();

        Task<CardEntry> GetByIdAsync(int id);

        Task<bool> CreateAsync(CardEntry cardEntry);

        Task<bool> UpdateAsync(int id, CardEntry updatedCardEntry);

        Task<bool> DeleteAsync(int id);

        Task<List<CardEntry>> GetAllAvailableForGameAsync(int gameId);
    }
}
