using System;
using System.Collections.Generic;

namespace DomainPsr03951.Models
{
    public  class Country
    {
        public Country()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
