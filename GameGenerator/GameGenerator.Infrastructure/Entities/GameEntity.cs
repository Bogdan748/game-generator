using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGenerator.Infrastructure.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CardEntity> Cards { get; set; }
    }
}
