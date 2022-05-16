using GameGenerator.Infrastructure.Entities.MapUsers;
using System.Collections.Generic;


namespace GameGenerator.Infrastructure.Entities.OnGoingGame
{
    public class OnGoingGameEntity
    {
        public int Id { get; set; }
        public string GameGroup { get; set; }
        public List<UserEntity> OnGoingUsers { get; set; }
        public List<OnGoingCardsEntity> OnGoingCards { get; set; }
    }
}
