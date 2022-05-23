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
    internal class OnGoingGameService : IOnGoingGameService
    {
        private readonly IOnGoingGameRepository _repository;

        public OnGoingGameService(IOnGoingGameRepository repository)
        {
            _repository = repository
                          ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> CreateAsync(OnGoingGameEntry onGoingGameEntry)
        {
            if (onGoingGameEntry is null)
            {
                throw new ArgumentException(nameof(onGoingGameEntry));
            }
            int affectedRows = await _repository.CreateAsync(onGoingGameEntry);
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

        public async Task<OnGoingGameEntry> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }
            OnGoingGameEntry entry = await _repository.GetByIdAsync(id);
            return entry;
        }

        public async Task<OnGoingGameEntry> GetByGroupAsync(string groupName)
        {
            if (groupName is null)
            {
                throw new ArgumentException($"Parameter {nameof(groupName)} must have a valid value");
            }
            OnGoingGameEntry entry = await _repository.GetByGroupAsync(groupName);
            return entry;
        }

        public async Task<int> UpdateAsync(int id, OnGoingGameEntry updatedOnGoingGameEntry)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            if (updatedOnGoingGameEntry is null)
            {
                throw new ArgumentException(nameof(updatedOnGoingGameEntry));
            }
            int affectedRows = await _repository.UpdateAsync(id, updatedOnGoingGameEntry);
            return affectedRows;
        }
    }
}
