using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Repositories.OnGoingGame
{
    public interface IOnGoingCardsRepository
    {
        Task<int> CreateAsync(OnGoingCardsEntry cardEntry);

        Task<int> UpdateAsync(int id, OnGoingCardsEntry updatedCardEntry);

        Task<int> DeleteAsync(int id);
    }
}
