using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class ActivityController : BaseController
    {

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                var activities = _db.Activities.ToList();
                return View(activities);
            }
            else
            {
                var activity = _db.Activities.Find(id);
                return View("ActivityIndex", activity);
            }
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var activity = new Activity();
            if (id != null)
            {
                activity = _db.Activities.Find(id);
            }
            ViewBag.Status = activity.Status.ToSelectList();
            return PartialView(activity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Activity activity)
        {
            if (activity.IsNew)
            {
                _db.Entry(activity).State = System.Data.EntityState.Added;
            }
            else
            {
                _db.Entry(activity).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult AddTask(int id)
        {
            var activity = new ActivityTask
            {
                ActivityId = id
            };
            ViewBag.Status = activity.Status.ToSelectList();
            return PartialView("EditTask", activity);
        }

        [HttpGet]
        public PartialViewResult EditTask(int id)
        {
            var activity = _db.ActivityTask.Find(id);
            ViewBag.Status = activity.Status.ToSelectList();
            return PartialView(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditTask(ActivityTask task, List<int> AcSeleUserIds)
        {
            ModelState.Clear();
            if ((AcSeleUserIds == null || AcSeleUserIds.Count == 0) && task.UserId == 0)
            {
                ModelState.AddModelError("", "Add Responsible Person");
            }
            else
            {
                var userId = AcSeleUserIds != null && AcSeleUserIds.Count > 0 ? AcSeleUserIds[0] : task.UserId;
                var user = _db.Users.Find(userId);
                task.UserName = user.FullName;
                task.UserId = user.UserId;
                if (task.IsNew)
                {
                    _db.Entry(task).State = System.Data.EntityState.Added;
                }
                else
                {
                    _db.Entry(task).State = System.Data.EntityState.Modified;
                }
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }


        [HttpGet]
        public JsonResult Remind(int id)
        {
            var task = _db.ActivityTask.Find(id);
            var user = _db.Users.Find(task.UserId);
            MailSrv.SendMail(user, task.ReminderMail(), "NITCAA | Task Reminder");
            return Json(new { }, JsonRequestBehavior.AllowGet); 
        }

    }
}
