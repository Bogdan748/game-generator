using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Models.OnGoingGame
{
    public class OnGoingGameEntry
    {
        public int Id { get; set; }
        public string GameGroup { get; set; }

        public int GameId { get; set; }
        public List<string> UserNames { get; set; }
        public List<int> OnGoingCardsIds { get; set; }
    }
}
