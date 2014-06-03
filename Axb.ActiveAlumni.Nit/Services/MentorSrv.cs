using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class MentorSrv : ThreadSrvBase, IDigestService
    {

        public string GetDigestTitle()
        {
            return "Mentoring";
        }

        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var msgs = GetMentorings(userId).Select(m => m.Messages.Last());
            var user = _db.Users.Find(userId);
            var newMsgs = msgs.Where(m => m.Date > user.TsMailDigest && m.SenderId != userId)
                            .Take(4);
            return newMsgs;
        }

        public static string GetUserMsg(MentorStatusType type)
        {
            switch (type)
            {
                case MentorStatusType.RequestSend:
                    return "Student had send mentoring request";
                case MentorStatusType.AdminApproved:
                    return "The mentoring request was approved by Admin";
                case MentorStatusType.AdminRejected:
                    return "The metoring request was rejected by Admin";
                case MentorStatusType.AlumniApproved:
                    return "The request was approved by Alumni.";
                case MentorStatusType.AlumniRejected:
                    return "The request was rejected by Alumni.";
                case MentorStatusType.StudentInfo:
                    return "The student is asked to provide more infomation about the project";
                case MentorStatusType.AlumniInfo:
                    return "The request was end to Alumni for more Info";
                case MentorStatusType.AdminInfo:
                    return "The request was send to Admin for more Info";
                case MentorStatusType.SuccessfullyCompleted:
                    return "SuccessfullyCompleted";
                case MentorStatusType.Terminated:
                default:
                    return type.ToString();
            }
        }

        public List<MentorShip> MyMentorships
        {
            get
            {
                var userId = UserSession.CurrentUserId;
                return GetMentorings(userId);
            }
        }

        private List<MentorShip> GetMentorings(int userId)
        {
            var mentorList = _db.MentorShips
                 .Include(m => m.Messages)
                 .Where(m => m.AlumniId == userId || m.StudentId == userId)
                 .OrderByDescending(m => m.StartDate)
                 .ToList();
            return mentorList;
 
        }

        public void MarkAsNotified()
        {
            var userId = UserSession.CurrentUserId;
            var user = _db.Users.Find(userId);
            user.TsMentorView = DateTime.UtcNow;
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        public List<NotifyDispItem> ComposeDispItems(IEnumerable<MentorShip> mentorships)
        {
            var userId = UserSession.CurrentUserId;
            var dispItems = new List<NotifyDispItem>();
            foreach (var item in mentorships)
            {
                var lastMsg = item.Messages.Last();
                var dispItem = new NotifyDispItem
                {
                    ItemId = item.EntityKey,
                    Date = lastMsg.Date,
                    Comment = lastMsg.Text,
                    UserId = lastMsg.SenderId,
                    UserName = lastMsg.SenderName,
                };
                if (lastMsg.SenderId == userId)
                {

                }
                dispItems.Add(dispItem);
            }
            return dispItems;
        }

        public override IEnumerable<ThreadBase> MyItems
        {
            get { return MyMentorships.Select(m => m as ThreadBase); }
        }

        public override DateTime LastViewd
        {
            get
            {
                var user = _db.Users.Find(UserSession.CurrentUserId);
                return user.TsMentorView;
            }
        }

        public override string ControllerName
        {
            get { return "Mentor"; }
        }

        public override bool NotifyCreateNew
        {
            get { return false; }
        }
    }
}