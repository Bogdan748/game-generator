using GameGenerator.Core.Models.OnGoingGame;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Services.OnGoingGame
{
    public interface IOnGoingGameService
    {
        Task<OnGoingGameEntry> GetByIdAsync(int id);

        Task<OnGoingGameEntry> GetByGroupAsync(string groupName);

        Task<int> CreateAsync(OnGoingGameEntry onGoingGameEntry);

        Task<int> UpdateAsync(int id, OnGoingGameEntry updatedOnGoingGameEntry);

        Task<int> DeleteAsync(int id);
    }
}
