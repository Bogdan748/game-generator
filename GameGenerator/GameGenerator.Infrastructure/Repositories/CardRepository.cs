using GameGenerator.Core.Exceptions;
using GameGenerator.Core.Models;
using GameGenerator.Core.Abstractions.Repositories;
using GameGenerator.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CardRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }


        public async Task<List<CardEntry>> GetAllAsync()
        {
            var cardEntities = await _applicationDbContext.CardEntity.ToListAsync();

            return cardEntities.Select(entity => entity.ToCardEntry())
                               .ToList();
        }

        public async Task<CardEntry> GetByIdAsync(int id)
        {
            var cardEntity = await _applicationDbContext.CardEntity.FirstOrDefaultAsync(c => c.Id == id);

            return cardEntity.ToCardEntry();
        }

        public async Task<int> CreateAsync(CardEntry cardEntry)
        {
            var cardEntity = new Entities.CardEntity()
            {
                Text = cardEntry.Text,
                CardType= cardEntry.CardType,
                GameId= cardEntry.GameId,
                
            };

            _applicationDbContext.Add(cardEntity);
            int affectedRows = await _applicationDbContext.SaveChangesAsync();

            return affectedRows;
        }

        public async Task<int> UpdateAsync(int id, CardEntry updatedCardEntry)
        {
            var cardEntity = await _applicationDbContext.CardEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (cardEntity is not null)
            {
                cardEntity.Text = updatedCardEntry.Text;
                cardEntity.Text = updatedCardEntry.Text;
                cardEntity.GameId = updatedCardEntry.GameId;

                try
                {
                    _applicationDbContext.Update(cardEntity);
                    int affectedRows = await _applicationDbContext.SaveChangesAsync();

                    return affectedRows;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    cardEntity = await _applicationDbContext.CardEntity.FirstOrDefaultAsync(c => c.Id == id);

                    if (cardEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating Card list entry", ex);
                    }

                }


            }

            return 0;
        }


        public async Task<int> DeleteAsync(int id)
        {
            var cardEntity = await _applicationDbContext.CardEntity.FirstOrDefaultAsync(c => c.Id == id);

            if (cardEntity is not null)
            {
                _applicationDbContext.Remove(cardEntity);
                int affectedRows = await _applicationDbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }
    }
}
