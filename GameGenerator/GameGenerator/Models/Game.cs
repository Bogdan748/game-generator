﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
