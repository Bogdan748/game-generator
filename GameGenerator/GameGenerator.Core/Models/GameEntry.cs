
using System.Collections.Generic;


namespace GameGenerator.Core.Models
{
    public class GameEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<CardEntry> Cards { get; set; }
    }
}
