using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Group
    {
        public Group()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? DeactivatedDate { get; set; }

        public ICollection<User> User { get; set; }
    }
}
