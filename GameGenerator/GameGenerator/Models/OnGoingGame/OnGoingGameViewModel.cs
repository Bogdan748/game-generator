using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models.OnGoingGame
{
    public class OnGoingGameViewModel
    {
        public int Id { get; set; }
        public string GameGroup { get; set; }

        public int GameId { get; set; }
        public int CurrentRound { get; set; }
        public List<string> UserNames { get; set; }
        public List<int> OnGoingCardsIds { get; set; }
    }
}
