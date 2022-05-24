using GameGenerator.Core.Abstractions.Repositories.OnGoingGame;
using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Infrastructure.Entities;
using GameGenerator.Infrastructure.Entities.MapUsers;
using GameGenerator.Infrastructure.Entities.OnGoingGame;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories.OnGoingGame
{
    public class OnGoingCardsRepository : IOnGoingCardsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OnGoingCardsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<List<OnGoingCardEntry>> GetByGroupAsync(string OnGoingGameGroup)
        {
            var onGoingCardEntities = await _applicationDbContext.OnGoingCardsEntity
                .Include(c => c.User)
                .Include(c => c.OnGoingGame)
                .Where(c => c.OnGoingGame.GameGroup == OnGoingGameGroup)
                .ToListAsync();
                
            return onGoingCardEntities.Select(o=>o.ToOnGoingCardEntry()).ToList();
        }

        public async Task<int> CreateAsync(OnGoingCardEntry onGoingcardEntry)
        {
            CardEntity cardEntity = await _applicationDbContext.CardEntity
                .FirstOrDefaultAsync(c => c.Id == onGoingcardEntry.CardId);

            UserEntity user = await _applicationDbContext.UserEntity
                .FirstOrDefaultAsync(u => u.UserName == onGoingcardEntry.UserName);

            OnGoingGameEntity onGoingGame = await _applicationDbContext.OnGoingGameEntity
                .FirstOrDefaultAsync(o => o.GameGroup == onGoingcardEntry.OnGoingGameGroup);


            if (cardEntity is null || user is null || onGoingGame is null)
            {
                throw new ArgumentNullException(nameof(cardEntity));
            }

            var onGoingcardEntity = new OnGoingCardsEntity()
            {
                Id = onGoingcardEntry.Id,
                CardId=cardEntity.Id,
                Round=onGoingcardEntry.Round,
                User=user,
                OnGoingGame=onGoingGame

            };

            _applicationDbContext.Add(onGoingcardEntity);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var onGoingcardEntity = await _applicationDbContext.OnGoingCardsEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (onGoingcardEntity is not null)
            {
                _applicationDbContext.Remove(onGoingcardEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }

        public async Task<int> UpdateAsync(int id, OnGoingCardEntry updatedCardEntry)
        {
            var onGoingcardEntity = await _applicationDbContext.OnGoingCardsEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (onGoingcardEntity is not null)
            {
                CardEntity cardEntity = await _applicationDbContext.CardEntity
                .FirstOrDefaultAsync(c => c.Id == updatedCardEntry.CardId);

                UserEntity user = await _applicationDbContext.UserEntity
                    .FirstOrDefaultAsync(u => u.UserName == updatedCardEntry.UserName);

                OnGoingGameEntity onGoingGame = await _applicationDbContext.OnGoingGameEntity
                    .FirstOrDefaultAsync(o => o.GameGroup == updatedCardEntry.OnGoingGameGroup);

                onGoingcardEntity.Round = updatedCardEntry.Round;
                onGoingcardEntity.CardId = updatedCardEntry.CardId;
                onGoingcardEntity.User = user;
                onGoingcardEntity.OnGoingGame = onGoingGame;

                try
                {
                    _applicationDbContext.Update(onGoingcardEntity);
                    int affectedRows = await _applicationDbContext.SaveChangesAsync();

                    return affectedRows;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    onGoingcardEntity = await _applicationDbContext.OnGoingCardsEntity.FirstOrDefaultAsync(c => c.Id == id);

                    if (onGoingcardEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating OnGoingCards list entry", ex);
                    }

                }
            }
            return 0;
        }
    }
}
