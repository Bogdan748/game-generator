﻿using GameGenerator.Core.Models.OnGoingGame;
using System.Threading.Tasks;

namespace GameGenerator.Core.Abstractions.Repositories.OnGoingGame
{
    public interface IOnGoingCardsRepository
    {
        Task<int> CreateAsync(OnGoingCardEntry cardEntry);

        Task<int> UpdateAsync(int id, OnGoingCardEntry updatedCardEntry);

        Task<int> DeleteAsync(int id);
    }
}