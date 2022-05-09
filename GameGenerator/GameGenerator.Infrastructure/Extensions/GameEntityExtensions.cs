using GameGenerator.Core.Models;
using GameGenerator.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Extensions
{
    public static class GameEntityExtensions
    {
        public static GameEntry ToGameEntry(this GameEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new GameEntry
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }


        public static CardEntry ToCardEntry(this CardEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new CardEntry
            {
                Id = entity.Id,
                Text=entity.Text,
                CardType=entity.CardType,
                GameId=entity.GameId
            };
        }
    }
}
