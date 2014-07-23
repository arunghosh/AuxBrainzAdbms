using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;
using System.Text;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class EventSrv : ThreadSrvBase, IDigestService
    {

        public string GetDigestTitle()
        {
            return "Upcoming Events";
        }

        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var events = GetEvents(userId);
            var upEvents = events.Where(m => m.FromDate > DateTime.UtcNow && !m.IsDeleted).OrderBy(e => e.FromDate).ToList();
            return upEvents;
        }


        public void UpdateStatus(int id, InviteStatusTypes status)
        {
            var user = _db.Users.Find(CurrUserId);
            var invitee = _db.EventInvitees.SingleOrDefault(i => i.UserId == user.UserId && i.EventId == id);
            if (invitee == null)
            {
                invitee = new EventInvitee
                    {
                        UserId = user.UserId,
                        UserName = user.FullName,
                        Status = status,
                        EventId = id
                    };
                _db.EventInvitees.Add(invitee);
            }
            else
            {
                invitee.Status = status;
                invitee.Date = DateTime.UtcNow;
                _db.Entry(invitee).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
        }

        public void SendSmsNotification(int eventId)
        {
            var evt = _db.Events.Include(e => e.Invitees).Single(e => e.EventId == eventId);
            var user = _db.Users.Find(CurrUserId);
            var comment = new EventComment
            {
                Text = "SMS notification send by " + user.FullName,
                SenderId = user.UserId,
                SenderName = user.FullName,
                EventId = eventId
            };
            _db.EventComments.Add(comment);
            _db.SaveChanges();

            var users = _db.Users.ToList();
            var ivtIds = evt.Invitees.Select(i => i.UserId).ToList();
            var code = evt.GetVisibilityCode();
            var invitees = users.Where(u => ivtIds.Contains(u.UserId) || ((u.GetVisibilityCode() & code)) > 0).ToList();
            using (var srv = new SmsSrv(invitees, evt.SmsMessage))
            {
                srv.SendSMSAsync();
            }
        }

        public void SendEmailNotification(int eventId)
        {
            var evt = _db.Events.Include(e => e.Invitees).Single(e => e.EventId == eventId);
            var user = _db.Users.Find(CurrUserId);
            var comment = new EventComment
            {
                Text = "Email notification send by " + user.FullName,
                SenderId = user.UserId,
                SenderName = user.FullName,
                EventId = eventId
            };
            _db.EventComments.Add(comment);
            _db.SaveChanges();

            var users = _db.Users.ToList();
            var ivtIds = evt.Invitees.Select(i => i.UserId).ToList();
            var code = evt.GetVisibilityCode();
            var invitees = users.Where(u => ivtIds.Contains(u.UserId) || ((u.GetVisibilityCode() & code)) > 0).ToList();
            MailSrv.SendMailToManyAsync(invitees, evt.EmailMessage, "NITCAA Event | " + evt.EventName);
        }

        public List<Event> GetUpcoming()
        {
            var events = MyEvents.Where(m => m.FromDate >= DateTime.UtcNow && !m.IsDeleted).OrderBy(e => e.FromDate).ToList();
            return events;
        }

        public List<Event> PastEvents()
        {
            var events = MyEvents.Where(m => m.FromDate < DateTime.UtcNow && !m.IsDeleted).OrderByDescending(e => e.FromDate).Take(3).ToList();
            return events;
        }

        public Event GetEvent(int id)
        {
            var model = _db.Events.Include(e => e.Invitees)
                    .Single(e => e.EventId == id);
            var userIds = model.Invitees.Select(i => i.UserId).ToList();
            userIds.Remove(CurrUserId);
            var users = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            model.Users = users;
            model.Visibilities = model.GroupsStr.ToList();
            return model;
        }

        public Event CreateOrUpdateEvent(Event info)
        {
            info.RemoveOffset();
            List<int> newUserIds = info.AcSeleUserIds;
            var currUser = _db.Users.Find(UserSession.CurrentUserId);
            newUserIds.Add(currUser.UserId);
            newUserIds = newUserIds.Distinct().ToList();

            info.UserGroups = info.GetVisibilityCode();

            info.CreatedUserId = currUser.UserId;
            info.CreatedUserName = currUser.FullName;

            if (info.EventId == 0)
            {
                info.Invitees = CreateInvitees(newUserIds);
                _db.Events.Add(info);
                var comment = new EventComment
                {
                    SenderId = currUser.UserId,
                    SenderName = currUser.FullName,
                    Text = "Event created by " + currUser.FullName
                };
                info.Comments = new List<EventComment> { comment };
            }
            else
            {
                UpdateInvitees(info, newUserIds);
                var comment = new EventComment
                {
                    SenderId = currUser.UserId,
                    SenderName = currUser.FullName,
                    Text = info.IsDeleted
                            ? "Event was cancelled by " + currUser.FullName
                            : "Event updated by " + currUser.FullName,
                    EventId = info.EventId
                };
                _db.EventComments.Add(comment);
                _db.Entry(info).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
            return info;
        }

        private void UpdateInvitees(Event info, IEnumerable<int> newList)
        {
            var oldInvites = _db.EventInvitees.Where(e => e.EventId == info.EventId).ToList();
            var oldList = oldInvites.Select(i => i.UserId).ToList();
            foreach (var newId in newList)
            {
                if (!oldList.Contains(newId))
                {
                    var user = _db.Users.Find(newId);
                    var map = new EventInvitee
                    {
                        UserId = user.UserId,
                        UserName = user.FullName,
                        EventId = info.EventId
                    };
                    _db.EventInvitees.Add(map);
                }
            }
            foreach (var old in oldInvites)
            {
                if (!newList.Contains(old.UserId))
                {
                    _db.Entry(old).State = System.Data.EntityState.Deleted;
                }
            }
            info.Invitees = null;
        }

        private List<EventInvitee> CreateInvitees(IEnumerable<int> userIds)
        {
            var maps = new List<EventInvitee>();
            var users = _db.Users.Where(u => userIds.Contains(u.UserId))
                            .ToList();
            foreach (var user in users)
            {
                maps.Add(new EventInvitee
                {
                    UserId = user.UserId,
                    UserName = user.FullName
                });
            }
            return maps;
        }

        public void MarkAsNotified()
        {
            var userId = UserSession.CurrentUserId;
            var user = _db.Users.Find(userId);
            user.TsEventView = DateTime.UtcNow;
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        public override IEnumerable<ThreadBase> MyItems
        {
            get
            {
                return MyEvents.Select(m => m);
            }
        }

        public IEnumerable<Event> MyEvents
        {
            get
            {
                int? userId = null;
                try
                {
                    userId = UserSession.CurrentUserId;
                }
                catch { }
                return GetEvents(userId);
            }
        }

        private List<Event> GetEvents(int? userId)
        {
            List<Event> events = new List<Event>();
            if (userId != null)
            {
                events = _db.Events
                    .Include(x => x.Invitees)
                    .Include(e => e.Comments)
                    .Where(e => e.Invitees.Any(i => i.UserId == userId) || (e.UserGroups != 0))
                    .ToList();
            }
            else
            {
                events = _db.Events
                    .Include(x => x.Invitees)
                    .Include(e => e.Comments)
                    .Where(e => ((e.UserGroups != 0) && !e.IsDeleted))
                    .ToList();
            }
            return events;
        }

        public override string ControllerName
        {
            get { return "Event"; }
        }

        public override DateTime LastViewd
        {
            get
            {
                var user = _db.Users.Find(UserSession.CurrentUserId);
                return user.TsEventView;
            }
        }

        public override bool NotifyCreateNew
        {
            get { return true; }
        }
    }
}