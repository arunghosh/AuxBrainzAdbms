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
    public class AlumniNews : GuestPostBase, IDigestEntity
    {
        [Key]
        public int AlumniNewsId { get; set; }

        [TitleLength]
        public string Title { get; set; }

        [TitleLength]
        public string SubTitle { get; set; }

        public string News { get; set; }

        [StringLength(256)]
        public string NewsLink { get; set; }

        public NewsType NewsType { get; set; }

        [StringLength(256)]
        public string ImageType { get; set; }

        public byte[] ImageData { get; set; }

        public override int EntityKey
        {
            get { return AlumniNewsId; }
        }

        [NotMapped]
        public string Slug
        {
            get
            {

                return Routes.GetSlug(Title);
            }
        }


        public string GetDisgest()
        {
            var composer = new HtmlComposer();
            composer.AppendImg(Routes.NewsImg(AlumniNewsId))
                    .AppendLinkHead(Title, Routes.NewsUrl(AlumniNewsId))
                    .AppendDiv(News.LetterLimited(120).Replace("\n", " "));
            return new HtmlComposer().AppendClearDiv(composer.Text).Text.ToString();
        }
    }
}