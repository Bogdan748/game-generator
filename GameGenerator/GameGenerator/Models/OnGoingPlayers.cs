using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models
{
    public class OnGoingPlayers
    { 
        public int Id { get; set; }
        [StringLength(100)]
        public string ConnectionId { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserType { get; set; }

        [ForeignKey("OnGoingGames")]
        public int GameCod { get; set; }

        public virtual OnGoingGames OnGoingGame { get; set; }
    }
}
