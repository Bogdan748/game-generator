using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models
{
    public class OnGoingCards
    {
        public int Id { get; set; }
        [ForeignKey("OnGoingGames")]
        public int GameCod { get; set; }
        
        [ForeignKey("Card")]
        public int CardId { get; set; }

        [ForeignKey("OnGoingPlayers")]
        public int CurrentPlayer { get; set; }
        public int Round { get; set; }
        public virtual OnGoingPlayers OnGoingPlayers { get; set; }
        public virtual OnGoingGames OnGoingGame { get; set; }
        public virtual Card Card { get; set; }

    }
}
