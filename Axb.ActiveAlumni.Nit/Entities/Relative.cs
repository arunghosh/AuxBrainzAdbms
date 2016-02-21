using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Relative:UserOwnedEntity
    {
        [Key]
        public int RelativeId { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [StringLength(64)]
        public string Location { get; set; }

        [StringLength(64)]
        [Required]
        public string Name { get; set; }

        [StringLength(128)]
        public string Work { get; set; }

        [StringLength(128)]
        public string Education { get; set; }

        [Required]
        public RelationType RelationShip { get; set; }

        public override int EntityKey
        {
            get { return RelativeId; }
        }
    }
}