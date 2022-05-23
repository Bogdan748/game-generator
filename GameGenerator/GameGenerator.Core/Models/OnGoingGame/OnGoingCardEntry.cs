using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Core.Models.OnGoingGame
{
    public class OnGoingCardEntry
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Round { get; set; }
        public string UserName { get; set; }
        public string OnGoingGameGroup { get; set; }
    }
}
