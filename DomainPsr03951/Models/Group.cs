using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainPsr03951.Models
{
    public  class Group
    {
        public Group()
        {
            User = new HashSet<User>();
        }
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        
        public bool IsInactive { get; set; }
        [Required]
        [DataType(DataType.DateTime)]

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
