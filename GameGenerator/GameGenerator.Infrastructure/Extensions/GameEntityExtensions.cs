using GameGenerator.Core.Models;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Infrastructure.Entities;
using GameGenerator.Infrastructure.Entities.MapUsers;
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
                Name = entity.Name,
                Cards = entity.Cards.Select(c => c.ToCardEntry()).ToList()
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
                GameId=entity.Game.Id
            };
        }


        public static UserEntry ToUserEntry(this UserEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new UserEntry
            {
                UserName=entity.UserName,
                UserType=entity.UserType,
                UserGroup=entity.UserGroup,
                Connections= entity.Connections.Select(c => c.ToConnectionEntry()).ToList()
            };
        }


        public static ConnectionEntry ToConnectionEntry(this ConnectionEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new ConnectionEntry
            {
                Connected=entity.Connected,
                ConnectionID=entity.ConnectionID,
                UserAgent=entity.UserAgent,
                UserName=entity.UserEntity.UserName
            };
        }
    }
}
