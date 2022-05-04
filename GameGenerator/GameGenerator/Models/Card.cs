using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CardType { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
