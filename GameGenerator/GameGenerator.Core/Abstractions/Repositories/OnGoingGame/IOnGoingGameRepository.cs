using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Repositories.OnGoingGame
{
    public interface IOnGoingGameRepository
    {
        Task<OnGoingGameEntry> GetByIdAsync(int id);

        Task<int> CreateAsync(OnGoingGameEntry cardEntry);

        Task<int> UpdateAsync(int id, OnGoingGameEntry updatedCardEntry);

        Task<int> DeleteAsync(int id);
    }
}
