using GameGenerator.Core.Abstractions.Repositories.OnGoingGame;
using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Infrastructure.Entities.OnGoingGame;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories.OnGoingGame
{
    public class OnGoingGameRepository : IOnGoingGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OnGoingGameRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<int> CreateAsync(OnGoingGameEntry onGoingGameEntry)
        {
            var gameEntity = new OnGoingGameEntity
            {
                GameGroup = onGoingGameEntry.GameGroup,
                Game = await _applicationDbContext.GameEntity.FirstOrDefaultAsync(a => a.Id == onGoingGameEntry.GameId)
            };

            _applicationDbContext.Add(gameEntity);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var onGoingGameEntity = await _applicationDbContext.OnGoingGameEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (onGoingGameEntity is not null)
            {
                _applicationDbContext.Remove(onGoingGameEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }

        public async Task<OnGoingGameEntry> GetByIdAsync(int id)
        {
            var onGoingGameEntity = await _applicationDbContext.OnGoingGameEntity
                .Include(o=>o.Game)
                .Include(o=>o.OnGoingCards)
                .Include(o => o.OnGoingUsers)
                .FirstOrDefaultAsync(c => c.Id == id);
                

            return onGoingGameEntity.ToOnGoingGameEntry();
        }

        public async Task<OnGoingGameEntry> GetByGroupAsync(string groupName)
        {
            var onGoingGameEntity = await _applicationDbContext.OnGoingGameEntity
                .Include(o => o.Game)
                .Include(o => o.OnGoingCards)
                .Include(o => o.OnGoingUsers)
                .FirstOrDefaultAsync(c => c.GameGroup == groupName);


            return onGoingGameEntity.ToOnGoingGameEntry();
        }

        public async Task<int> UpdateAsync(int id, OnGoingGameEntry updatedOnGoingGameEntry)
        {
            var onGoingGameEntity = await _applicationDbContext.OnGoingGameEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (onGoingGameEntity is not null)
            {
                onGoingGameEntity.GameGroup = updatedOnGoingGameEntry.GameGroup;


                try
                {
                    _applicationDbContext.Update(onGoingGameEntity);
                    int affectedRows = await _applicationDbContext.SaveChangesAsync();

                    return affectedRows;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    onGoingGameEntity = await _applicationDbContext.OnGoingGameEntity.FirstOrDefaultAsync(c => c.Id == id);

                    if (onGoingGameEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating OnGoingGame list entry", ex);
                    }

                }


            }

            return 0;
        }
    }
}
