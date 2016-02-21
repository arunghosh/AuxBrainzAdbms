using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class FeedbackController : BaseController
    {
        //
        // GET: /Admin/Feedback/

        [AllowAnonymous]
        public PartialViewResult New()
        {
            FillAuthKeys();
            return PartialView(new Feedback());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ReadStatus(int id)
        {
            var item = _db.Feedbacks.Find(id);
            item.IsRead = !item.IsRead;
            _db.Entry(item).State = System.Data.EntityState.Modified;
            _db.SaveChanges();
            var stat = item.IsRead ? "Mark as Unread" : "Mark as Read";
            return Json(stat);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult New(Feedback model)
        {
            if (ModelState.IsValid)
            {
                model.IPAddress = Request.UserHostAddress;
                if (IsAuth)
                {
                    var curr = CurrentUser;
                    model.UserId = curr.UserId;
                    model.UserName = curr.FullName;
                }
                _db.Feedbacks.Add(model);
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        [AxbAuthorize(Roles = Constants.RoleAdmin)]
        public ViewResult Index()
        {
            var feeds = _db.Feedbacks.ToList();
            return View(feeds);
        }
    }
}
