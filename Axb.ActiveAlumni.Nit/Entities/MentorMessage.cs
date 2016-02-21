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
    public class MentorMessage : ThreadItemBase, IDigestEntity
    {
        [Key]
        public int MentorMessageId { get; set; }

        public MentorStatusType Status { get; set; }

        [ForeignKey("MentorShip")]
        public int MentorShipId { get; set; }
        public virtual MentorShip MentorShip { get; set; }

        public override int EntityKey
        {
            get { return MentorMessageId; }
        }

        public override int ParentId
        {
            get { return MentorShipId; }
        }

        public string GetDisgest()
        {
            var composer = new HtmlComposer();
            composer.AppendLinkHead(SenderName, Routes.MentorUrl(MentorShipId))
                    .AppendDiv(Text.LetterLimited(80));
            return composer.Text.ToString();

        }
    }
}