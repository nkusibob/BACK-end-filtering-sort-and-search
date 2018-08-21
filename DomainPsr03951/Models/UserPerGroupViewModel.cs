using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainPsr03951.Models
{
    public class UserPerGroupViewModel
    {
        public int idGroup { get; set; }
        public string Name { get; set; }
        public int IdUser { get; set; }
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
