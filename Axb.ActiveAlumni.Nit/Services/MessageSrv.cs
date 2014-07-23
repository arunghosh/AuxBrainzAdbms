using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class MessageSrv : ThreadSrvBase, IDigestService
    {
        #region Get / Notification

        public string GetDigestTitle()
        {
            return "Messages";
        }

        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var msgs = GetMessages(userId).Select(m => m.Messages.Last());
            var user = _db.Users.Find(userId);
            var newMsgs = msgs.Where(m => m.Date > user.TsMailDigest && m.SenderId != userId)
                            .Take(4);
            return newMsgs;
        }

        public List<MessageThread> MyMesssageThreads
        {
            get
            {
                var userId = UserSession.CurrentUserId;
                return GetMessages(userId);
            }
        }

        private List<MessageThread> GetMessages(int userId)
        {
            var msgThreads = _db.MessageThreads
                    .Include(mt => mt.MessageUserMaps)
                    .Include(mt => mt.Messages)
                    .Where(mt => mt.MessageUserMaps.Any(mp => mp.UserId == userId))
                    .ToList()
                    .OrderByDescending(t => t.Messages.Last().Date)
                    .ToList();
            return msgThreads;
        }

        public void MarkAsNotified()
        {
            var userId = UserSession.CurrentUserId;
            var user = _db.Users.Find(userId);
            user.TsMessageView = DateTime.UtcNow;
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        #endregion

        #region Creation

        public void MessageMany(string text, List<int> receiverIds)
        {
            var currUser = _db.Users.Find(UserSession.CurrentUserId);
            receiverIds.Add(currUser.UserId);
            receiverIds = receiverIds.Distinct().ToList();
            MessageThread thread = CreateThread(receiverIds);
            AddMessageToThread(text, thread.MessageThreadId);
        }

        public void MessageOne(string text, int receiverId)
        {
            var currUser = _db.Users.Find(UserSession.CurrentUserId);
            MessageThread thread = GetDualThread(currUser.UserId, receiverId);
            AddMessageToThread(text, thread.MessageThreadId);
        }

        public void AddMessageToThread(string message, int threadId)
        {
            var user = _db.Users.Find(UserSession.CurrentUserId);
            var thread = _db.MessageUserMaps.SingleOrDefault(m => m.MessageThreadId == threadId && m.UserId == user.UserId);
            if (thread == null)
            {
                throw new UnauthorisedExcpetion();
            }
            var msg = new Message
            {
                SenderId = user.UserId,
                SenderName = user.FullName,
                Text = message,
                Date = DateTime.UtcNow,
                MessageThreadId = threadId,
            };

            _db.Messages.Add(msg);
            _db.SaveChanges();
        }

        private MessageThread GetDualThread(int senderId, int receiverId)
        {
            var userMsgs = _db.MessageThreads
                                    .Where(m => m.MessageUserMaps.Count == 2
                                            && m.MessageUserMaps.Any(mp => mp.UserId == senderId)
                                            && m.MessageUserMaps.Any(mp => mp.UserId == receiverId))
                                    .ToList();

            MessageThread thread = userMsgs.Any()
                                    ? userMsgs.First()
                                    : CreateThread(new List<int> { senderId, receiverId });
            return thread;
        }

        private MessageThread CreateThread(IEnumerable<int> userIds)
        {
            var thread = new MessageThread();
            var users = _db.Users.Where(u => userIds.Contains(u.UserId))
                            .ToList();
            foreach (var user in users)
            {
                thread.MessageUserMaps.Add(new MessageUserMap
                {
                    UserId = user.UserId,
                    UserName = user.FullName
                });
            }
            _db.MessageThreads.Add(thread);
            _db.SaveChanges();
            return thread;
        }

        #endregion

        public override IEnumerable<ThreadBase> MyItems
        {
            get { return MyMesssageThreads.Select(m => m as ThreadBase); }
        }

        public override DateTime LastViewd
        {
            get
            {
                var user = _db.Users.Find(UserSession.CurrentUserId);
                return user.TsMessageView;
            }
        }

        public override string ControllerName
        {
            get { return "Message"; }
        }

        public override bool NotifyCreateNew
        {
            get { return true; }
        }
    }
}