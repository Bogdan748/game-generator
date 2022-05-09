
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        //public virtual GameViewModel Game { get; set; }

    }
}
