using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class ServiceInfo : UserOwnedEntity
    {
        [Key]
        public int ServiceInfoId { get; set; }

        [StringLength(128)]
        [Required]
        [Display(Name="Service")]
        public string Title { get; set; }

        [StringLength(64)]
        [Required]
        public string Category { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        [StringLength(128)]
        [Required]
        public string Location { get; set; }

        public DateTime CreateOn { get; set; }

        [Phone]
        [StringLength(32)]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(64)]
        public string Email { get; set; }

        public override int EntityKey
        {
            get { return ServiceInfoId; }
        }

        public ServiceInfo()
        {
            CreateOn = DateTime.UtcNow;
        }
    }
}