using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class UserRole : UserOwnedEntity
    {
        [Key]
        public int UserRoleId { get; set; }
        
        public UserRoleType RoleType { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public UserRole()
        {
            CreatedOn = DateTime.UtcNow;
            IsActive = true;
        }

        public override int EntityKey
        {
            get { return UserRoleId; }
        }
    }
}