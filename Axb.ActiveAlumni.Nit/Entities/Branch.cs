using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [StringLength(256)]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        [StringLength(24)]
        public string Abbrevation { get; set; }

        public int StartedOnYear { get; set; }

        public byte Status { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual ICollection<Institution> Insitutions { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}