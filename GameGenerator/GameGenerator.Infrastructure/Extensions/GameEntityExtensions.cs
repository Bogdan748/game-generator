using GameGenerator.Core.Models;
using GameGenerator.Core.Models.MapUsers;
using GameGenerator.Core.Models.OnGoingGame;
using GameGenerator.Infrastructure.Entities;
using GameGenerator.Infrastructure.Entities.MapUsers;
using GameGenerator.Infrastructure.Entities.OnGoingGame;
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

        public static OnGoingGameEntry ToOnGoingGameEntry(this OnGoingGameEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new OnGoingGameEntry
            {
                Id = entity.Id,
                GameGroup = entity.GameGroup,
                UsersNamePoints = entity.OnGoingUsers.Where(x=>x.UserType!="admin").ToDictionary(x=>x.UserName,y=>y.Points),
                OnGoingCardsIds = entity.OnGoingCards.Select(c => c.Id).ToList(),
                CurrentRound=entity.CurrentRound,
                GameId = entity.Game.Id
            };
        }

        public static OnGoingGameEntity ToOnGoingGameEntity(this OnGoingGameEntry entry)
        {
            if (entry is null)
            {
                return null;
            }

            return new OnGoingGameEntity
            {
                Id = entry.Id,
                GameGroup = entry.GameGroup
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

        public static OnGoingCardEntry ToOnGoingCardEntry(this OnGoingCardsEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new OnGoingCardEntry
            {
                Id = entity.Id,
                CardId=entity.CardId,
                Round=entity.Round,
                UserName=entity.User.UserName,
                OnGoingGameGroup=entity.OnGoingGame.GameGroup
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
                Points=entity.Points,
                OnGoingGameEntry=entity.OnGoingGameEntity.ToOnGoingGameEntry(),
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
