using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public abstract class ThreadItemBase: EntityBase
    {
        [Required]
        public int SenderId { get; set; }

        [FullNameLength]
        [Required]
        public string SenderName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [MessageLength]
        public string Text { get; set; }

        [NotMapped]
        public abstract int ParentId
        {
            get;
        }

        public ThreadItemBase()
        {
            Date = DateTime.UtcNow;
        }
    }
}