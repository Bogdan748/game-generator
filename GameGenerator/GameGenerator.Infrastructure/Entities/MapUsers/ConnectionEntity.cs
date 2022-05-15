
namespace GameGenerator.Infrastructure.Entities.MapUsers
{
    public class ConnectionEntity
    {
        public string ConnectionID { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }

        public UserEntity UserEntity { get; set; }
    }
}
