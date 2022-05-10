using GameGenerator.Core.Models;
using GameGenerator.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameGenerator.Core.Abstractions.Services;

namespace GameGenerator.Core.Services
{
    internal class CardService : ICardService
    {
        private readonly ICardRepository _repository;

        public CardService(ICardRepository repository)
        {
            _repository = repository
                          ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<CardEntry>> GetAllAsync()
        {
            List<CardEntry> cardEntries = await _repository.GetAllAsync();
            return cardEntries;
        }

        public async Task<CardEntry> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }
            CardEntry entry = await _repository.GetByIdAsync(id);
            return entry;
        }

        public async Task<bool> CreateAsync(CardEntry cardEntry)
        {
            if (cardEntry is null)
            {
                throw new ArgumentException(nameof(cardEntry));
            }
            int affectedRows = await _repository.CreateAsync(cardEntry);
            return affectedRows == 1;
        }

        public async Task<bool> UpdateAsync(int id, CardEntry updatedCardEntry)
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
            return affectedRows == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            int affectedRows = await _repository.DeleteAsync(id);
            return affectedRows == 1;
        }
    }
}
