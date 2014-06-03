using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class AlumniArticleBase: UserOwnedEntity
    {
        public int AlumniId { get; set; }

        [StringLength(124)]
        public string Course { get; set; }

        public string Batch { get; set; }

        [FullNameLength]
        public string AlumniName { get; set; }

        public abstract string About { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public AlumniArticleBase()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}