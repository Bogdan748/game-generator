using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models.OnGoingGame
{
    public class OnGoingCardViewModel
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Round { get; set; }
        public string UserName { get; set; }
        public int OnGoingGameId { get; set; }
    }
}
