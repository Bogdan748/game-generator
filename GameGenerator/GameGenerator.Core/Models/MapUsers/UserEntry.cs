using System.Collections.Generic;

namespace GameGenerator.Core.Models.MapUsers
{
    public class UserEntry
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string UserGroup { get; set; }

        public OnGoingGame.OnGoingGameEntry OnGoingGameEntry { get; set; }
        public List<ConnectionEntry> Connections { get; set; }
    }
}
