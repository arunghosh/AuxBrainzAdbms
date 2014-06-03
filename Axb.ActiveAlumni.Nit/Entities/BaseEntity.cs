using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class EntityBase
    {
        [NotMapped]
        public abstract int EntityKey
        {
            get;
        }


        [NotMapped]
        public bool IsNew
        {
            get
            {
                return EntityKey == 0;
            }
        }

    }

    public abstract class UserOwnedEntity: EntityBase
    {
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}