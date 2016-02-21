using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class FailedLogin: EntityBase, IReadEntity
    {
        [Key]
        public int FailedLoginId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

        public DateTime Time { get; set; }

        [StringLength(128)]
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public override int EntityKey
        {
            get { return FailedLoginId; }
        }
    }
}