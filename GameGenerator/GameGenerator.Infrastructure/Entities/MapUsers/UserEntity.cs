using System.Collections.Generic;


namespace GameGenerator.Infrastructure.Entities.MapUsers
{
    public class UserEntity
    {
        
        public string UserName { get; set; }
        public string UserType { get; set; }

        public string UserGroup { get; set; }
        public List<ConnectionEntity> Connections { get; set; }
    }
}
