using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainPsr03951.Models
{
    public partial class Rejoint
    {
        [Key]
        public int id { get; set; }
        public int IdGroup { get; set; }
        public int idUser { get; set; }
        
    }
}
