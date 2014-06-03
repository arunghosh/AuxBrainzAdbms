using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class AlumniSpeak : AlumniArticleBase
    {
        [Key]
        public int AlumniToKnowId { get; set; }

        public override int EntityKey
        {
            get { return AlumniToKnowId; }
        }

        public AlumniSpeak()
            :base()
        {
        }

        [StringLength(8096)]
        public override string About
        {
            get;
            set;
        }
    }
}