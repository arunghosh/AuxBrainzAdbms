using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Education: UserOwnedEntity
    {
        [Key]
        public int EducationId { get; set; }

        [StringLength(128)]
        [Required]
        [Display(Name="Institution/School")]
        public string SchoolName { get; set; }

        [Display(Name="Year of graduation")]
        public string Batch { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(64)]
        public string Degree { get; set; }

        [StringLength(128)]
        [Display(Name = "Specialisation")]
        public string Specialisation { get; set; }

        public override int EntityKey
        {
            get { return EducationId; }
        }
    }
}