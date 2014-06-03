using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Circle: EntityBase
    {
        [Key]
        public int CircleId { get; set; }

        public int UserId { get; set; }

        [FullNameLength]
        public string Name { get; set; }

        public virtual List<User> Members { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public override int EntityKey
        {
            get { return CircleId; }
        }

        public Circle()
        {
            Members = new List<User>();
            Date = DateTime.UtcNow;
        }
    }
}