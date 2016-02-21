using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class DiscussionSrv : ThreadSrvBase, IDigestService
    {
        public override List<NotifyDispItem> ComposeDispItems(IEnumerable<ThreadBase> msgThreads)
        {
            var userId = UserSession.CurrentUserId;
            var dispItems = new List<NotifyDispItem>();
            foreach (var item in msgThreads)
            {
                var lastMsg = item.ThreadItems.Last();
                var dispItem = new NotifyDispItem
                {
                    ItemId = item.EntityKey,
                    Title = item.ThreadTitle,
                    Date = lastMsg.Date,
                    Comment = lastMsg.Text,
                    UserId = lastMsg.SenderId,
                    UserName = lastMsg.SenderName,
                    IsReply = true
                };
                if (lastMsg.SenderId == userId)
                {
                    var map = item.EntityUserMaps.First(mp => mp.UserId != userId);
                    dispItem.UserId = map.UserId;
                    dispItem.UserName = map.UserName;
                }
                if (item.ThreadItems.Count() == 1)
                {
                    dispItem.Comment = (item as Discussion).Content;
                    dispItem.IsReply = false;
                }
                dispItems.Add(dispItem);
            }
            return dispItems;
        }

        public string GetDigestTitle()
        {
            return "Discussions";
        }

        public IEnumerable<IDigestEntity> GetDigest(int userId)
        {
            var user = _db.Users.Find(userId);
            var discussions = GetDiscussions(userId)
                                    .Where(d => d.Comments.Last().Date > user.TsMailDigest)
                                    .Take(3)
                                    .ToList();
            return discussions;
        }

        public IEnumerable<Discussion> RecentDiscussion()
        {
            return MyDiscussions.Take(5);
        }

        public IEnumerable<Discussion> MostCommented()
        {
            return MyDiscussions.OrderByDescending(c => c.Comments.Count).Take(5);
        }

        public IEnumerable<Discussion> RecentBlogs()
        {
            var blogs = _db.Discussions.Where(d => !d.IsDeleted && d.DiscusionType == DiscusionTypes.Blog)
                                .ToList();
            blogs.Reverse();
            return blogs.Take(5);

        }

        public Discussion CreateDiscussion(Discussion pDisc)
        {
            var userIds = GetUserIds(pDisc);
            pDisc.UserMap = CreateUserMaps(userIds);
            pDisc.Tags = String.Join(",", pDisc.SelectedTags) + "," + pDisc.TagCsv;
            pDisc.DiscussionCrowd = pDisc.GetVisibilityCode();
            pDisc.Comments = new List<DiscussionComment> { CreateComment("###Main###") };
            _db.Discussions.Add(pDisc);
            _db.SaveChanges();
            return pDisc;
        }


        public Discussion CreateOrUpdateDiscussion(Discussion pDisc)
        {
            if (pDisc.IsNew)
            {
                return CreateDiscussion(pDisc);
            }
            else
            {
                return UpdateDiscussion(pDisc);
            }
        }


        public Discussion UpdateDiscussion(Discussion pDisc)
        {
            var newIds = GetUserIds(pDisc);
            UpdateInvitees(pDisc, newIds);
            pDisc.Tags = String.Join(",", pDisc.SelectedTags) + "," + pDisc.TagCsv;
            pDisc.DiscussionCrowd = pDisc.GetVisibilityCode();
            _db.Entry(pDisc).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            return pDisc;
        }


        private void UpdateInvitees(Discussion pDisc, IEnumerable<int> newList)
        {
            var oldInvites = _db.EventInvitees.Where(e => e.EventId == pDisc.DiscussionId).ToList();
            var oldIds = oldInvites.Select(i => i.UserId).ToList();
            foreach (var newId in newList)
            {
                if (!oldIds.Contains(newId))
                {
                    var user = _db.Users.Find(newId);
                    var map = new DiscussionUserMap
                    {
                        UserId = user.UserId,
                        UserName = user.FullName,
                        DiscussionId = pDisc.DiscussionId
                    };
                    _db.DiscussionUserMaps.Add(map);
                }
            }
            foreach (var old in oldInvites)
            {
                if (!newList.Contains(old.UserId))
                {
                    _db.Entry(old).State = System.Data.EntityState.Deleted;
                }
            }
            pDisc.UserMap = null;
        }

        protected List<int> GetUserIds(Discussion pDisc)
        {
            List<int> userIds = pDisc.AcSeleUserIds;
            foreach (var crse in pDisc.AcCourses)
            {
                userIds = userIds.Concat(GetUserFromAcCourse(crse)).ToList();
            }
            var currUser = _db.Users.Find(UserSession.CurrentUserId);
            userIds.Add(currUser.UserId);
            userIds = userIds.Distinct().ToList();
            return userIds;
        }

        private List<int> GetUserFromAcCourse(string acCourse)
        {
            var batch = acCourse.Substring(0, 4);
            var course = acCourse.Remove(0, 5);
            if (course.Contains("#"))
            {
                var ids = DbCache.UserSearch.Where(u => u.UserCourses.Any(c => c.Batch == batch))
                            .Select(u => u.UserId)
                            .ToList();
                return ids;
            }
            else
            {
                var ids = DbCache.UserSearch.Where(u => u.UserCourses.Any(c => c.Batch == batch && c.BranchName == course))
                            .Select(u => u.UserId)
                            .ToList();
                return ids;
            }
        }

        private DiscussionComment CreateComment(string message)
        {
            var user = _db.Users.Find(UserSession.CurrentUserId);
            var comment = new DiscussionComment
            {
                Date = DateTime.UtcNow,
                Text = message,
                SenderId = user.UserId,
                SenderName = user.FullName,
            };
            return comment;
        }

        private List<DiscussionUserMap> CreateUserMaps(IEnumerable<int> userIds)
        {
            var maps = new List<DiscussionUserMap>();
            var users = _db.Users.Where(u => userIds.Contains(u.UserId))
                            .ToList();
            foreach (var user in users)
            {
                maps.Add(new DiscussionUserMap
                {
                    UserId = user.UserId,
                    UserName = user.FullName
                });
            }
            return maps;
        }

        public List<Discussion> MyDiscussions
        {
            get
            {
                var discussions = GetDiscussions(UserSession.CurrentUserId);
                return discussions;
            }
        }

        private List<Discussion> GetBlogs()
        {
            var user = _db.Users.Find(CurrUserId);

            var discussions = _db.Discussions
                                .Include(d => d.Comments)
                                .Where(mt => (!mt.IsDeleted && mt.DiscusionType == DiscusionTypes.Blog))
                                .ToList()
                                .OrderByDescending(t => t.Comments.Last().Date)
                                .ToList();
            return discussions;
        }

        private List<Discussion> GetDiscussions(int userId)
        {
            var user = _db.Users.Find(userId);
            byte code = 0;
            if (user.IsAlumni()) code |= (0x01 << (byte)DiscussCrowdType.Alumni);
            if (user.IsStudent()) code |= (0x01 << (byte)DiscussCrowdType.Student);
            if (user.IsStaff()) code |= (0x01 << (byte)DiscussCrowdType.Staff);

            var discussions = _db.Discussions
                                .Include(d => d.UserMap)
                                .Include(d => d.Comments)
                                .ToList()
                                .Where(mt => (!mt.IsDeleted && mt.DiscusionType == DiscusionTypes.Discussion && ((mt.DiscussionCrowd & code) != 0 || mt.UserMap.Any(mp => mp.UserId == userId) || user.IsAdmin())))
                                .OrderByDescending(t => t.Comments.Last().Date)
                                .ToList();
            return discussions;
        }

        public void MarkAsNotified()
        {
            var userId = UserSession.CurrentUserId;
            var user = _db.Users.Find(userId);
            user.TsDiscussionView = DateTime.UtcNow;
            _db.Entry(user).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
        }

        public void AddComment(string comment, int discussionId)
        {
            var user = _db.Users.Find(UserSession.CurrentUserId);
            var msg = new DiscussionComment
            {
                SenderId = user.UserId,
                SenderName = user.FullName,
                Text = comment,
                Date = DateTime.UtcNow,
                DiscussionId = discussionId,
            };

            _db.DiscussionComments.Add(msg);
            _db.SaveChanges();
        }

        public override IEnumerable<ThreadBase> MyItems
        {
            get { return MyDiscussions.Select(m => m); }
        }

        public override DateTime LastViewd
        {
            get
            {
                var user = _db.Users.Find(UserSession.CurrentUserId);
                return user.TsDiscussionView;
            }
        }

        public override string ControllerName
        {
            get { return "Discussion"; }
        }

        public override bool NotifyCreateNew
        {
            get { return true; }
        }
    }
}