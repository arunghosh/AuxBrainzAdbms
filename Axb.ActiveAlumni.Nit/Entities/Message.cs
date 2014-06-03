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
    public class Message: ThreadItemBase, IDigestEntity
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("MessageThread")]
        public int MessageThreadId { get; set; }
        
        public virtual MessageThread MessageThread { get; set; }

        public override int EntityKey
        {
            get { return MessageId; }
        }

        public override int ParentId
        {
            get { return MessageThreadId; }
        }

        public string GetDisgest()
        {
            var composer = new HtmlComposer();
            composer.AppendImg(Routes.ImageUrl("msg.png"))
                    .AppendLinkHead(SenderName, Routes.MessageUrl(MessageThreadId))
                    .AppendDiv(Text.LetterLimited(80).Replace("\n", " "));
            return composer.Text.ToString();

        }
    }
}