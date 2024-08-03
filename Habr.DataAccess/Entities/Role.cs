using Habr.DataAccess.Enums;

namespace Habr.DataAccess.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleType RoleType { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
