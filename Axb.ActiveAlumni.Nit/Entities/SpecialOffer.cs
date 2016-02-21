using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class SpecialOffer : GuestPostBase
    {
        [Key]
        public int SpecialOfferId { get; set; }

        [StringLength(256)]
        [Required]
        public string OrganisationName { get; set; }

        [StringLength(512)]
        public string Address { get; set; }

        [StringLength(1024)]
        [Required]
        public string OfferStatment { get; set; }

        [StringLength(64)]
        [Required]
        public string City { get; set; }

        [StringLength(64)]
        public string Country { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(256)]
        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        [StringLength(1024)]
        [Display(Name = "Google Map Link of the Location")]
        public string GoogleMap { get; set; }

        [StringLength(64)]
        [Required]
        [Display(Name = "Category (Hotel / Restaurant / Resort )")]
        public string Category { get; set; }

        public override int EntityKey
        {
            get { return SpecialOfferId; }
        }
    }
}