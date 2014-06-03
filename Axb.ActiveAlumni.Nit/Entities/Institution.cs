using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public partial class Institution
    {
        [Key]
        public int InstitutionId { get; set; }
        
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string City { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        [StringLength(256)]
        public string Pincode { get; set; }

        [StringLength(256)]
        public string State { get; set; }

        [StringLength(256)]
        public string Country { get; set; }

        [Display(Name = "Started On")]
        [Range(1800, 2030)]
        public int StartYear { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
