﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string CardType { get; set; }
        public int GameId { get; set; }
        public GameEntity Game { get; set; }
    }
}