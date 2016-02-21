using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Address: UserOwnedEntity
    {
        [Key]
        public int AddressId { get; set; }

        [StringLength(128)]
        public string AddressLine1 { get; set; }

        [StringLength(64)]
        [Required]
        public string City { get; set; }

        [StringLength(64)]
        public string State { get; set; }

        [StringLength(64)]
        [Required]
        public string Country { get; set; }

        [StringLength(32)]
        public string Pincode { get; set; }

        public override int EntityKey
        {
            get { return AddressId; }
        }

        public AddressType AddressType { get; set; }
    }
}