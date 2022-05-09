using GameGenerator.Core.Models;
using GameGenerator.Core.Repositories;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace GameGenerator.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repository;

        public GameService(IGameRepository repository)
        {
            _repository = repository 
                          ?? throw new ArgumentNullException(nameof(repository));
        }
        
        public async Task<List<GameEntry>> GetAllAsync()
        {
            List<GameEntry> gameEntries = await _repository.GetAllAsync();
            return gameEntries;
        }

        public async Task<GameEntry> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }
            GameEntry entry = await _repository.GetByIdAsync(id);
            return entry;
        }

        public async Task<bool> CreateAsync(GameEntry gameEntry)
        {
            if (gameEntry is null)
            {
                throw new ArgumentException(nameof(gameEntry));
            }
            int affectedRows = await _repository.CreateAsync(gameEntry);
            return affectedRows == 1;
        }

        public async Task<bool> UpdateAsync(int id, GameEntry updatedGameEntry)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            if (updatedGameEntry is null)
            {
                throw new ArgumentException(nameof(updatedGameEntry));
            }
            int affectedRows = await _repository.UpdateAsync(id,updatedGameEntry);
            return affectedRows==1;
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
