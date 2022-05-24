using GameGenerator.Core.Abstractions.Repositories.OnGoingGame;
using GameGenerator.Core.Abstractions.Services.OnGoingGame;
using GameGenerator.Core.Models.OnGoingGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Services.OnGoingGame
{
    internal class OnGoingCardService : IOnGoingCardService
    {
        private readonly IOnGoingCardsRepository _repository;

        public OnGoingCardService(IOnGoingCardsRepository repository)
        {
            _repository = repository
                          ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<OnGoingCardEntry>> GetByGroupAsync(string OnGoingGameGroup)
        {
            if (OnGoingGameGroup is null)
            {
                throw new ArgumentException($"Parameter {nameof(OnGoingGameGroup)} must have a valid value");
            }
            List<OnGoingCardEntry> entry = await _repository.GetByGroupAsync(OnGoingGameGroup);
            return entry;
        }
        public async Task<int> CreateAsync(OnGoingCardEntry cardEntry)
        {
            if (cardEntry is null)
            {
                throw new ArgumentException(nameof(cardEntry));
            }
            int affectedRows = await _repository.CreateAsync(cardEntry);
            return affectedRows;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            int affectedRows = await _repository.DeleteAsync(id);
            return affectedRows;
        }

        public async Task<int> UpdateAsync(int id, OnGoingCardEntry updatedCardEntry)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            if (updatedCardEntry is null)
            {
                throw new ArgumentException(nameof(updatedCardEntry));
            }
            int affectedRows = await _repository.UpdateAsync(id, updatedCardEntry);
            return affectedRows;
        }
    }
}
