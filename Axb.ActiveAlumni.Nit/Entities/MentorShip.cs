using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class MentorShip : ThreadBase
    {
        [Key]
        public int MentorShipId { get; set; }

        public int AlumniId { get; set; }
        
        public int StudentId { get; set; }

        [FullNameLength]
        [Required]
        public string AlumniName { get; set; }

        [FullNameLength]
        [Required]
        public string StudentName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public MentorStatusType Status { get; set; }

        public virtual List<MentorMessage> Messages { get; set; }

        public override int EntityKey
        {
            get { return MentorShipId; }
        }

        public MentorShip()
        {
            Status = MentorStatusType.RequestSend;
        }

        public override IEnumerable<ThreadItemBase> ThreadItems
        {
            get { return Messages.Select(m => m as ThreadItemBase); }
        }

        public override IEnumerable<EntityUserMap> EntityUserMaps
        {
            get
            {
                return new List<EntityUserMap>
                {
                    new MentorUserMap
                    {
                        UserId = StudentId,
                        UserName = StudentName
                    },
                    new MentorUserMap
                    {
                        UserId = AlumniId,
                        UserName = AlumniName
                    }
                };
            }
        }
    }
}