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
    public class Event : ThreadBase, IDigestEntity
    {
        public int EventId { get; set; }

        public bool IsTentative { get; set; }

        [StringLength(512)]
        public string SpecificGroupName { get; set; }

        [StringLength(10)]
        public string Batch { get; set; }

        [StringLength(1024)]
        public string ExternalLink { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime CreatedOn { get; set; }

        [FullNameLength]
        public string CreatedUserName { get; set; }

        [Required]
        [TitleLength]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Display(Name = "Event starts at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Display(Name = "Events ends at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [StringLength(512)]
        [Display(Name = "Event Location")]
        [Required]
        public string Location { get; set; }

        [StringLength(1024)]
        [Display(Name = "Register Link")]
        public string GoogleMap { get; set; }

        [MessageLength]
        [Display(Name = "Details")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public byte UserGroups { get; set; }

        [Url]
        [StringLength(2000)]
        public string PhotoUrl { get; set; }

        public override int EntityKey
        {
            get { return EventId; }
        }

        public virtual List<EventComment> Comments { get; set; }

        public virtual List<EventInvitee> Invitees { get; set; }

        //public virtual List<EventMinute> Minutes { get; set; }

        #region Groups

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

        public List<DiscussCrowdType> GroupsEnum
        {
            get
            {
                var visibles = new List<DiscussCrowdType>();
                foreach (DiscussCrowdType item in Enum.GetValues(typeof(DiscussCrowdType)))
                {
                    byte code = (byte)(0x01 << (byte)item);
                    if ((UserGroups & code) != 0)
                    {
                        visibles.Add(item);
                    }
                }
                return visibles;
            }
        }

        public IEnumerable<string> GroupsStr
        {
            get
            {
                var group = GroupsEnum.Any() ? GroupsEnum.Select(c => c.ToString()).ToList() : new List<string>();
                return group;
            }
        }

        public IEnumerable<string> GroupsDisplay
        {
            get
            {
                var group = GroupsEnum.Any() ? GroupsEnum.Select(c => c.ToString()).ToList() : new List<string>();
                var specGrp = DiscussCrowdType.SpecificGroup.ToString();
                if (group.Contains(specGrp))
                {
                    group.Remove(specGrp);
                    group.Add(SpecificGroupName);
                }
                return group;
            }
        }

        #endregion

        public override string ThreadTitle
        {
            get
            {
                return EventName;
            }
        }

        public override IEnumerable<ThreadItemBase> ThreadItems
        {
            get { return Comments.Select(m => m as ThreadItemBase); }
        }

        public override IEnumerable<EntityUserMap> EntityUserMaps
        {
            get { return Invitees.Select(m => m as EntityUserMap); }
        }

        [NotMapped]
        public List<Poll> Polls { get; set; }

        [NotMapped]
        public int TimeOffset { get; set; }

        public Event()
        {
            CreatedOn = DateTime.UtcNow;
            VisibilityOptions = Enum.GetNames(typeof(DiscussCrowdType)).Cast<string>();
            Visibilities = new List<string>();
            AcSeleUserIds = new List<int>();
            Invitees = new List<EventInvitee>();
            ToDate = FromDate = DateTime.UtcNow;
        }

        public void SetOffset(int offset)
        {
            TimeOffset = offset;
            ToDate = ToDate.AddMinutes(-1 * TimeOffset);
            FromDate = FromDate.AddMinutes(-1 * TimeOffset);
        }

        public void RemoveOffset()
        {
            ToDate = ToDate.AddMinutes(TimeOffset);
            FromDate = FromDate.AddMinutes(TimeOffset);
        }

        #region Not Mapped

        [NotMapped]
        public string Slug
        {
            get
            {
                return Routes.GetSlug(EventName);
            }
        }

        [NotMapped]
        public List<EventInvitee> Going
        {
            get
            {
                return Invitees.Where(i => i.Status == InviteStatusTypes.Going).ToList();
            }
        }
        [NotMapped]
        public List<EventInvitee> MayBe
        {
            get
            {
                return Invitees.Where(i => i.Status == InviteStatusTypes.MayBe).ToList();
            }
        }
        [NotMapped]
        public List<EventInvitee> NotGoing
        {
            get
            {
                return Invitees.Where(i => i.Status == InviteStatusTypes.NotGoing).ToList();
            }
        }

        [NotMapped]
        public int GoingCnt 
        {
            get
            {
                return Going.Count;
            }
        }

        [NotMapped]
        public int NotGoingCnt
        {
            get
            {
                return NotGoing.Count;
            }
        }

        [NotMapped]
        public int MayBeCnt
        {
            get
            {
                return MayBe.Count;
            }
        }

        [NotMapped]
        public List<User> Users { get; set; }

        [NotMapped]
        public IEnumerable<string> VisibilityOptions { get; set; }

        [NotMapped]
        public List<string> Visibilities { get; set; }

        [NotMapped]
        public List<int> AcSeleUserIds { get; set; }

        [NotMapped]
        public bool IsOneDay
        {
            get
            {
                return FromDate.ToIst().Date == ToDate.ToIst().Date;
            }
        }

        [NotMapped]
        public string Status
        {
            get
            {
                if (IsDeleted) return "cancelled";
                if (FromDate > DateTime.UtcNow)
                {
                    return "upcoming";
                }
                else
                {
                    return "over";
                }
            }
        }

        [NotMapped]
        public string EmailMessage
        {
            get
            {
                FromDate = FromDate.ToIst();
                ToDate = ToDate.ToIst();
                var from = string.Format("{0} {1} IST", FromDate.ToString("ddd, dd MMM yyyy"), FromDate.ToShortTimeString());
                var to = string.Format("{0} {1} IST", ToDate.ToShortDateString(), ToDate.ToShortTimeString());
                var duration = (ToDate - FromDate).TotalHours;
                var msg = string.Format("  Event: <b>{0}</b><br/>  Location: <b>{1}</b>\n  Starts at: <b>{2}</b>\n  Duration: {3}Hrs\n",
                    EventName, Location.Replace("\n", " ").Replace("\r", " "), from, duration);
                msg = Comments.Last().Text + "\n\n" + msg;

                msg += ("\nFor more details visit " + Routes.EventsUrl(EventId) + "\n");
                return msg;
            }
        }

        [NotMapped]
        public string SmsMessage
        {
            get
            {
                FromDate = FromDate.ToIst();
                ToDate = ToDate.ToIst();
                var from = string.Format("{0} {1} IST", FromDate.ToString("ddd, dd MMM yyyy"), FromDate.ToShortTimeString());
                var to = string.Format("{0} {1} IST", ToDate.ToShortDateString(), ToDate.ToShortTimeString());
                var duration = (ToDate - FromDate).TotalHours;
                var msg = string.Format("{4}: {0}\nLocation: {1}\nStarts at: {2}\nDuration: {3}Hrs",
                    EventName, Location, from, duration, Comments.Last().Text);
                msg += ("\nFor details " + Routes.EventsUrl(EventId) + "\n");
                return msg;
            }
        }

        #endregion

        public string GetDisgest()
        {
            var istFrom = FromDate.Add(TimeSpan.FromMinutes(330));
            var from = string.Format("{0} {1} IST", FromDate.ToString("ddd, dd MMM yyyy"), FromDate.ToShortTimeString());
            var composer = new HtmlComposer();
            composer.AppendImg(Routes.ImageUrl("event_mail.jpg"))
                    . AppendLinkHead(EventName, Routes.EventsUrl(EventId))
                    .AppendDiv(Location.Replace("\n", " "))
                    .AppendDiv(from);
            return composer.Text.ToString();
        }
    }
}