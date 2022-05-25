using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameGenerator.Models.MapUsers
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string UserGroup { get; set; }

        public int Points { get; set; }
    }
}
