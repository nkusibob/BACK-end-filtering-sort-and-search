using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainPsr03951.Models
{
    public  class User
    {
        [Key]
        public int id { get; set; }
       
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [Required]
         
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        public string CreationDate_FORMAT
        {
            get
            {
                return CreationDate.ToString("dd/MM/yyyy");
            }
        }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Gender { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }

        [Required]

        

        public bool IsInactive { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? DeactiveDate { get; set; }
        public string DeactiveDate_FORMAT
        {
            get
            {
                return DeactiveDate?.ToString("dd/MM/yyyy");
            }
        }
        [DataType(DataType.ImageUrl)]

        public string GravatarUrl { get; set; }
     
        [ForeignKey("Group")]
        public int IdGroup { get; set; }

        public Country Country { get; set; }
        public Group IdGroupNavigation { get; set; }
    }
}
