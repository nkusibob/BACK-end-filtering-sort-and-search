using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainPsr03951.Models
{
    class UserViewModel
    {
        [Key]
        public int id { get; set; }
        public int CountryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public string EmailAdress { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsInactive { get; set; }
        public DateTime? DeactiveDate { get; set; }
        public string GravatarUrl { get; set; }
        public int? IdGroup { get; set; }

    }
}
