using GameGenerator.Core.Models.OnGoingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Services.OnGoingGame
{
    public interface IOnGoingCardService
    {
        Task<List<OnGoingCardEntry>> GetByGroupAsync(string OnGoingGameGroup);
        Task<int> CreateAsync(OnGoingCardEntry cardEntry);

        Task<int> UpdateAsync(int id, OnGoingCardEntry updatedCardEntry);

        Task<int> DeleteAsync(int id);
    }
}
