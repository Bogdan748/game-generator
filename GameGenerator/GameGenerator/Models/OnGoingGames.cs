using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models
{
    public class OnGoingGames
    {
        [Key]
        public int GameCod { get; set; }

        [ForeignKey("Game")]
        public int GameTypeId { get; set; }

        public virtual ICollection<OnGoingPlayers> OnGoingPlayers { get; set; }
        public virtual ICollection<OnGoingCards> Cards { get; set; }

        public virtual Game Game { get; set; }

    }
}
