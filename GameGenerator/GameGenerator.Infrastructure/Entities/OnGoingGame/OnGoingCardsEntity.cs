using GameGenerator.Infrastructure.Entities.MapUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Entities.OnGoingGame
{
    public class OnGoingCardsEntity
    {
        public int Id { get; set; }
        public CardEntity Card { get; set; }
        public int Round { get; set; }
        public UserEntity User { get; set; }
        public OnGoingGameEntity OnGoingGame { get; set; }
    }
}
