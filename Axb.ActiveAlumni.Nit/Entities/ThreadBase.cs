using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class ThreadBase: EntityBase
    {
        [NotMapped]
        public abstract IEnumerable<ThreadItemBase> ThreadItems
        {
            get;
        }

        [NotMapped]
        public virtual string ThreadTitle
        {
            get
            {
                return string.Empty;
            }
        }

        [NotMapped]
        public abstract IEnumerable<EntityUserMap> EntityUserMaps
        {
            get;
        }
    }
}