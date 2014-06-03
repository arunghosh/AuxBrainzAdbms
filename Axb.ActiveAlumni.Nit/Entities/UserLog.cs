using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public class UserLog : UserOwnedEntity
    {
        [Key]
        public int UserLogId { get; set; }

        public DateTime Date { get; set; }

        [StringLength(1024)]
        public string Comment { get; set; }

        public int ByUserId { get; set; }

        [FullNameLength]
        public string ByUserName { get; set; }

        public override int EntityKey
        {
            get { return UserLogId; }
        }

        public UserLog()
        {
            Init();
        }

        private void Init()
        {
            Date = DateTime.UtcNow;
        }

        public UserLog(User by, int userId)
        {
            ByUserId = by.UserId;
            ByUserName = by.FullName;
            UserId = userId;
            Init();
        }
    }
}