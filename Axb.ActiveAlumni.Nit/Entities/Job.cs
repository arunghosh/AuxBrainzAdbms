using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Job : UserOwnedEntity
    {
        [Key]
        public int JobId { get; set; }
    
        [Required]
        [StringLength(128)]
        public string CompanyName { get; set; }

        [StringLength(128)]
        public string Location { get; set; }

        [StringLength(64)]
        public string Position { get; set; }

        [StringLength(64)]
        public string Domain { get; set; }

        [Display(Name="I currently work here")]
        public bool IsCurrentEmployer { get; set; }

        public override int EntityKey
        {
            get { return JobId; }
        }
    }
}