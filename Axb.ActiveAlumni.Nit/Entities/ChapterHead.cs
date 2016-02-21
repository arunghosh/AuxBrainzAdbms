using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class ChapterHead: EntityBase
    {
        [Key]
        public int ChapterHeadId { get; set; }

        [StringLength(32)]
        public string Position { get; set; }

        public int UserId { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        [FullNameLength]
        [NotMapped]
        public string Name { get; set; }

        [StringLength(10)]
        public string Batch { get; set; }

        [StringLength(64)]
        public string Branch { get; set; }

        [Required]
        public int ChapterId { get; set; }

        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }

        [NotMapped]
        public List<int> AcSeleUserIds { get; set; }

        public bool IsDeleted { get; set; }

        public void SetUser(User user)
        {
            UserName = user.FullName;
            Batch = user.UserCourses[0].Batch;
            Branch = user.UserCourses[0].BranchName;
            UserId = user.UserId;
        }

        public ChapterHead()
        {
            UserId = 0;
        }

        public override int EntityKey
        {
            get { return ChapterHeadId; }
        }
    }
}