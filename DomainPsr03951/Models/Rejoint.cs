using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainPsr03951.Models
{
    public partial class Rejoint
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Group")]
        public int IdGroup { get; set; }
        [ForeignKey("user")]
        public int idUser { get; set; }
        
    }
}
