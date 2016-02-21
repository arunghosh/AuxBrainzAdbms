using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class JobPosition
    {
        [Key]
        public int JobPositionId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Abbrevation { get; set; }
    }
}