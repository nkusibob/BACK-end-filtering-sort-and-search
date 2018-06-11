using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class User
    {
        public int Id { get; set; }
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

        public Group IdGroupNavigation { get; set; }
    }
}
