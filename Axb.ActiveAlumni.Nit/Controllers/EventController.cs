using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.ViewModels;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class EventController : BaseController
    {
        private EventSrv _srv = new EventSrv();

        [HttpGet]
        public PartialViewResult EmailNotify(int id)
        {
            var evnt = _db.Events.Find(id);
            return PartialView(evnt);
        }

        [HttpGet]
        public PartialViewResult SmsNotify(int id)
        {
            var evnt = _db.Events.Find(id);
            return PartialView(evnt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SmsNotify(Event model)
        {
            _srv.SendSmsNotification(model.EventId);
            return Json("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EmailNotify(Event model)
        {
            _srv.SendEmailNotification(model.EventId);
            return Json("");
        }

        [HttpGet]
        public PartialViewResult InviteStat(int id)
        {
            var evt = _db.Events.Include(e => e.Invitees).Single(e => e.EventId == id);
            return PartialView(evt);
        }

        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            CurrentPage = PageTypes.Events;
            ListDisplayVm<Event> model = null;
            var events = _srv.MyEvents.OrderBy(e => e.FromDate).OrderByDescending(e => e.Status);
            if (events.Any())
            {
                model = new ListDisplayVm<Event>
                {
                    SelectedId = id ?? events.First().EventId,
                    Items = events.ToList()
                };
                var sele = events.ToList().Find(l => l.EventId == model.SelectedId);

                ViewBag.Meta = sele.EventName;
                ViewBag.MetaD = sele.Location;
                ViewBag.OgImg = Routes.ImageUrl("eventFb.jpg");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Show(int id)
        {
            var evt = _db.Events.Include(e => e.Comments)
                        .Include(e => e.Invitees)
                        .Single(e => e.EventId == id);
            return PartialView(evt);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Upcoming()
        {
            var model = _srv.GetUpcoming();
            return PartialView(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Past()
        {
            var model = _srv.PastEvents();
            return PartialView("Upcoming", model);
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            ViewData[Constants.IsAdminKey] = CurrentUser.IsAdmin();
            return PartialView("New", _srv.GetEvent(id));
        }

        [HttpPost]
        public PartialViewResult New(List<int> userIds)
        {
            Event model = new Event();
            userIds = userIds == null ? new List<int>() : userIds;
            userIds.Remove(CurrentUserId);
            var users = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            model.Users = users;
            ViewData[Constants.IsAdminKey] = CurrentUser.IsAdmin();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult CreateNew(Event model)
        {
            if (model.FromDate > model.ToDate)
            {
                ModelState.AddModelError("", "Event end date should be greater than event start date");
            }

            if (!model.AcSeleUserIds.Any() && !model.Visibilities.Any())
            {
                ModelState.AddModelError("", "Invite at least one user");
            }

            ModelState["FromDate"].Errors.Clear();
            ModelState["ToDate"].Errors.Clear();

            if (model.EventId != 0)
            {
                var evt = _db.Events.Find(model.EventId);
                var currUser = CurrentUser;
                if (evt.Comments[0].SenderId != currUser.UserId && !currUser.IsAdmin())
                {
                    LogUnAuth();
                }
            }

            if (ModelState.IsValid)
            {
                _srv.CreateOrUpdateEvent(model);
                return Json(new { url = "/Events/" + model.EventId });
            }

            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(EventInvitee model)
        {
            _srv.UpdateStatus(model.EventId, model.Status);
            var evnt = _db.Events.Include(e => e.Invitees).Single(e => e.EventId == model.EventId);
            var result = new
            {
                gnCnt = evnt.GoingCnt,
                ngCnt = evnt.NotGoingCnt,
                mbCnt = evnt.MayBeCnt,
            };
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            try
            {
                if (_srv != null)
                {
                    _srv.Dispose();
                }
            }
            catch { }
        }
    }
}
