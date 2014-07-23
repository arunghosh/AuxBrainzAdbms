using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class Poll : EntityBase
    {
        [Key]
        public int PollId { get; set; }

        [StringLength(4096)]
        public string Question { get; set; }

        public virtual List<PollInvitee> Invitees { get; set; }

        public virtual List<PollOption> Options { get; set; }

        public bool IsPublic { get; set; }

        public byte UserGroups { get; set; }

        public PollTypes PollType { get; set; }

        public int PollTypeId { get; set; }

        public override int EntityKey
        {
            get { return PollId; }
        }
    }

    public class PollInvitee : EntityUserMap
    {
        [Key]
        public int PollInviteeId { get; set; }

        public override int EntityKey
        {
            get { return PollInviteeId; }
        }
    }

    public enum PollTypes
    {
        Normal = 0,
        Event = 1
    }

    public class PollVote : EntityBase
    {
        public int PollVoteId { get; set; }

        public int UserId { get; set; }

        [FullNameLength]
        public string UserName { get; set; }

        [StringLength(32)]
        public string IPAddress { get; set; }

        public DateTime TimeStamp { get; set; }

        [ForeignKey("PollOptionId")]
        public virtual PollOption Option { get; set; }

        public int PollOptionId { get; set; }

        public PollVote()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public override int EntityKey
        {
            get { return PollVoteId; }
        }
    }

    public class PollOption : UserOwnedEntity
    {
        [Key]
        public int PollOptionId { get; set; }

        [StringLength(1024)]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public int PollId { get; set; }

        [ForeignKey("PollId")]
        public virtual Poll Poll { get; set; }

        public virtual List<PollVote> Votes { get; set; }

        public override int EntityKey
        {
            get { return PollOptionId; }
        }

        public PollOption()
        {
            Date = DateTime.UtcNow;
        }
    }
}