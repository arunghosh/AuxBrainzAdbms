using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class DiscussionController : BaseController
    {
        DiscussionSrv _srv = new DiscussionSrv();

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult BlogSummary()
        {
            return PartialView(_srv.RecentBlogs());
        }

        [HttpGet]
        public ViewResult BlogEdit(int? id)
        {
            var model = id == null ? new Discussion() : _db.Discussions.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ViewResult BlogEdit(Discussion model)
        {
            if (ModelState.IsValid)
            {
                model.DiscusionType = DiscusionTypes.Blog;
                var updatedDisc = _srv.CreateOrUpdateDiscussion(model);
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult Recent()
        {
            return PartialView("Links", _srv.RecentDiscussion());
        }

        [HttpGet]
        public PartialViewResult MostCommented()
        {
            return PartialView("Links", _srv.MostCommented());
        }

        [HttpGet]
        public ViewResult Index(int? id)
        {
            if (id == null)
            {
                var threads = _srv.MyDiscussions;
                var dispItems = _srv.ComposeDispItems(threads);
                if (dispItems.Any())
                {
                    id = id ?? dispItems.First().ItemId;
                }
            }
            FillAuthKeys();
            return View(id);
        }

        [HttpGet]
        public PartialViewResult Comments(int id)
        {
            var model = _db.DiscussionComments.Where(c => c.DiscussionId == id).ToList().Skip(1).ToList();
            FillAuthKeys();
            return PartialView("Comments", model);
        }


        [HttpGet]
        public PartialViewResult Show(int id)
        {
            var model = _db.Discussions
                    .Include(m => m.UserMap)
                    .SingleOrDefault(m => m.DiscussionId == id);
            FillAuthKeys();
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var disc = _db.Discussions
                                .Include(x => x.Comments)
                                .Include(x => x.UserMap)
                                .Single(d => d.DiscussionId == id);
            var ids = disc.UserMap.Select(u => u.UserId).ToList();
            disc.Users = _db.Users.Where(u => ids.Contains(u.UserId)).ToList();
            disc.Visibilities = disc.GroupsStr.ToList();
            return PartialView("New", disc);
        }

        [HttpPost]
        public PartialViewResult New(List<int> userIds)
        {
            userIds = userIds == null ? new List<int>() : userIds;
            userIds.Remove(CurrentUserId);
            var users = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            return PartialView(new Discussion
                {
                    Users = users,
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOrUpdate(Discussion model)
        {
            if (!model.AcSeleUserIds.Any() && !model.Visibilities.Any() && !model.AcCourses.Any())
            {
                ModelState.AddModelError("", "Include at least one group or user.");
            }

            if (model.DiscussionId != 0)
            {
                var evt = _db.Discussions.Find(model.DiscussionId);
                var currUser = CurrentUser;
                if (evt.Comments[0].SenderId != currUser.UserId && !currUser.IsAdmin())
                {
                    LogUnAuth();
                }
            }

            if (ModelState.IsValid)
            {
                model.DiscusionType = DiscusionTypes.Discussion;
                var updatedDisc = _srv.CreateOrUpdateDiscussion(model);
                return Json(new { url = "/Discussions/" + updatedDisc.DiscussionId });
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Hide(int id)
        {
            var user = CurrentUser;
            var discussion = _db.Discussions
                                .Include(c => c.Comments)
                                .Single(d => d.DiscussionId == id);
            if (user.IsAdmin() || discussion.Comments[0].SenderId == user.UserId)
            {
                discussion.DeletedBy = user.UserId;
                discussion.DeletedOn = DateTime.UtcNow;
                discussion.IsDeleted = !discussion.IsDeleted;
                _db.Entry(discussion).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                LogUnAuth();
            }
            return Json(discussion.IsDeleted ? "Unhide Discussion" : "Hide Discussion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string msg, int id)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                try
                {
                    _srv.AddComment(msg, id);
                    ModelState.Clear();
                    return Comments(id);
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to send message");
                }
            }
            else
            {
                ModelState.AddModelError("", "Comment cannot be empty");
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateAffinity(CommentAffinity model)
        {
            if (model.Status == AffinityStatus.Delete)
            {
                return DeleteComment(model);
            }
            else
            {
                return UpdateAffinityCnt(model);
            }
        }

        private JsonResult DeleteComment(CommentAffinity model)
        {
            var comment = _db.DiscussionComments.Find(model.DiscussionCommentId);
            comment.IsDeleted = !comment.IsDeleted;
            _db.Entry(comment).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            var json = new
            {
                deleted = comment.IsDeleted ? "Undo Hide" : "Hide",
            };
            return Json(json);
        }

        private JsonResult UpdateAffinityCnt(CommentAffinity model)
        {
            var user = CurrentUser;
            model.UserId = user.UserId;
            model.UserName = user.FullName;
            var aff = _db.CommentAffinities.SingleOrDefault(c => c.UserId == model.UserId && c.DiscussionCommentId == model.DiscussionCommentId);
            if (aff == null)
            {
                _db.CommentAffinities.Add(model);
                _db.SaveChanges();
            }
            else
            {
                aff.Status = model.Status;
                aff.TimeStamp = aff.TimeStamp;
                _db.Entry(aff).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
            }
            var comment = _db.DiscussionComments
                            .Include(c => c.Affinities)
                            .Single(d => d.DiscussionCommentId == model.DiscussionCommentId);
            var json = new
            {
                agree = comment.AgreeCnt,
                disagree = comment.DisagreeCnt,
                offensive = comment.OffensiveCnt,
            };
            return Json(json);
        }

        [AllowAnonymous]
        public PartialViewResult FrontPage()
        {
            var dis = _db.Discussions.ToList().Last();
            return PartialView(dis);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                _srv.Dispose();
            }
            catch { }
            base.Dispose(disposing);
        }
    }
}
