using System.Collections.Generic;

namespace Charcoal.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Velocity { get; set; }
        public List<User> Users { get; set; }
    }
}