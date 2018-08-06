﻿using System;
using System.Collections.Generic;

namespace DomainPsr03951.Models
{
    public  class Group
    {
        public Group()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public string DeactiveDate_FORMAT
        {
            get
            {
                return DeactivatedDate?.ToString("dd/MM/yyyy");
            }
        }
        public ICollection<User> User { get; set; }
    }
}
