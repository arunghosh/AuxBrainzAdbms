using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class NonAdminNews: GuestPostBase, IReadEntity
    {
        [Key]
        public int NonAdminNewsId { get; set; }

        [StringLength(124)]
        [Required]
        public string Course { get; set; }

        [StringLength(10)]
        [Required]
        public string Batch { get; set; }

        [FullNameLength]
        [Required (ErrorMessage="The name of the person is required")]
        public string AlumniName { get; set; }

        [StringLength(2048)]
        [Required]
        public string News { get; set; }

        public override int EntityKey
        {
            get { return NonAdminNewsId; }
        }

        public bool IsRead { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

    }
}