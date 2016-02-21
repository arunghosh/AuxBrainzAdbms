using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class EntityUserMap : EntityBase
    {
        public int UserId { get; set; }

        [Required]
        [FullNameLength]
        public string UserName { get; set; }

    }
}