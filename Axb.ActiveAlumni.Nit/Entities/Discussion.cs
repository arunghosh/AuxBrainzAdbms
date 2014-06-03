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
    public class Discussion : ThreadBase, IDigestEntity
    {
        public Discussion()
        {
            VisibilityOptions = Enum.GetNames(typeof(DiscussCrowdType)).Cast<string>();
            Visibilities = new List<string>();
            AcCourses = new List<string>();
            SelectedTags = new List<string>();
            AcSeleUserIds = new List<int>();
            DiscusionType = DiscusionTypes.Discussion;
        }

        [Key]
        public int DiscussionId { get; set; }

        [TitleLength]
        public string Title { get; set; }

        [StringLength(4084)]
        [Required]
        public string Content { get; set; }

        [StringLength(256)]
        public string Tags { get; set; }

        public byte DiscussionCrowd { get; set; }

        public IEnumerable<string> GetCrowd()
        {
            return GetCrowdEnum().Select(c => c.ToString());
        }

        public virtual List<DiscussionUserMap> UserMap { get; set; }

        public virtual List<DiscussionComment> Comments { get; set; }

        public override IEnumerable<ThreadItemBase> ThreadItems
        {
            get { return Comments.Select(m => m as ThreadItemBase); }
        }

        public override IEnumerable<EntityUserMap> EntityUserMaps
        {
            get { return UserMap.Select(m => m as EntityUserMap); }
        }

        public bool IsDeleted { get; set; }

        public int DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public override string ThreadTitle
        {
            get
            {
                return Title;
            }
        }

        public DiscusionTypes DiscusionType { get; set; }

        public override int EntityKey
        {
            get { return DiscussionId; }
        }

        public string GetDisgest()
        {
            var composer = new HtmlComposer();
            var comment = Comments[0];
            composer.AppendImg(Routes.ImageUrl("discussion.png"))
                    .AppendLinkHead(Title, Routes.DisucssionUrl(DiscussionId))
                    .AppendDiv("by " + comment.SenderName, true)
                    .AppendDiv(Content.LetterLimited(120).Replace("\n", " "));
            return composer.Text.ToString();
        }

        #region Not Mapped

        [NotMapped]
        public List<int> AcSeleUserIds { get; set; }
        
        [NotMapped]
        public List<string> Visibilities { get; set; }

        [NotMapped]
        public IEnumerable<string> VisibilityOptions { get; set; }
        
        [NotMapped]
        public List<string> AcCourses { get; set; }

        [NotMapped]
        public List<string> TagOptions { get; set; }

        [NotMapped]
        public List<string> SelectedTags { get; set; }

        [NotMapped]
        public string TagCsv { get; set; }

        [NotMapped]
        public List<User> Users { get; set; }

        [NotMapped]
        public string Slug 
        {
            get
            {

                return Routes.GetSlug(Title);
            }
        }


        [NotMapped]
        public IEnumerable<string> GroupsStr
        {
            get
            {
                var group = GroupsEnum.Any() ? GroupsEnum.Select(c => c.ToString()).ToList() : new List<string>();
                return group;
            }
        }

        public List<DiscussCrowdType> GroupsEnum
        {
            get
            {
                var visibles = new List<DiscussCrowdType>();
                foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
                {
                    byte code = (byte)(0x01 << (byte)item);
                    if ((DiscussionCrowd & code) != 0)
                    {
                        visibles.Add(item);
                    }
                }
                return visibles;
            }
        }

        public List<DiscussCrowdType> GetCrowdEnum()
        {
            var visibles = new List<DiscussCrowdType>();
            foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
            {
                byte code = (byte)(0x01 << (byte)item);
                if ((DiscussionCrowd & code) != 0)
                {
                    visibles.Add(item);
                }
            }
            return visibles;
        }

        public byte GetVisibilityCode()
        {
            byte code = 0;
            if (Visibilities == null) return code;
            foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
            {
                if (Visibilities.Contains(item.ToString()))
                {
                    code |= (byte)(0x01 << (byte)item);
                }
            }
            return code;
        }

        #endregion
    }
}