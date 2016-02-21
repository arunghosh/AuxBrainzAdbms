using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public partial class Course
    {
        public int CourseId { get; set; }

        [StringLength(256)]
        [Display(Name="Course Name")]
        public string Name { get; set; }
        
        [StringLength(24)]
        public string Abbrevation { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
