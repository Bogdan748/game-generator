using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Models;
using GameGenerator.Core.Abstractions.Repositories;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }


        public async Task<List<GameEntry>> GetAllAsync()
        {
            var gameEntities = await _applicationDbContext.GameEntity.ToListAsync();

            return gameEntities.Select(entity => entity.ToGameEntry())
                               .ToList();
        }

        public async  Task<GameEntry> GetByIdAsync(int id)
        {
            var gameEntity = await _applicationDbContext.GameEntity.FirstOrDefaultAsync(c => c.Id == id);

            return gameEntity.ToGameEntry();
        }

        public async Task<int> CreateAsync(GameEntry gameEntry)
        {
            var gameEntity = new Entities.GameEntity()
            {
                Name = gameEntry.Name
                
            };

            _applicationDbContext.Add(gameEntity);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> UpdateAsync(int id, GameEntry updatedGameEntry)
        {
            var gameEntity = await _applicationDbContext.GameEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (gameEntity is not null)
            {
                gameEntity.Name = updatedGameEntry.Name;
                

                try
                {
                    _applicationDbContext.Update(gameEntity);
                    int affectedRows = await _applicationDbContext.SaveChangesAsync();

                    return affectedRows;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    gameEntity = await _applicationDbContext.GameEntity.FirstOrDefaultAsync(c => c.Id == id);

                    if(gameEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating Game list entry", ex);
                    }
                    
                }

                
            }

            return 0;
        }


        public async Task<int> DeleteAsync(int id)
        {
            var gameEntity = await _applicationDbContext.GameEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (gameEntity is not null)
            {
                _applicationDbContext.Remove(gameEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }
    }
}
