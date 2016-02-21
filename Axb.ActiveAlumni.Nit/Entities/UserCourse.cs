using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class UserCourse: UserOwnedEntity
    {
        [Key]
        public int UserCourseId { get; set; }

        [StringLength(10)]
        [Required]
        [Display(Name = "Graduated Year")]
        public string Batch { get; set; }

        [StringLength(128)]
        public string BranchName { get; set; }

        [StringLength(128)]
        public string CourseName { get; set; }

        [ForeignKey("Branch")]
        [Required]
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public override int EntityKey
        {
            get
            {
                return UserCourseId;
            }
        }

        public UserCourse()
        {
            BranchId = 1;
        }

        public void UpdateCourseNames()
        {
            var branch = DbCache.Branches.Single(b => b.BranchId == BranchId);
            var course = DbCache.Courses.Single(c => c.CourseId == branch.CourseId);
            BranchName = branch.Name;
            CourseName = course.Name;
        }
    }
}