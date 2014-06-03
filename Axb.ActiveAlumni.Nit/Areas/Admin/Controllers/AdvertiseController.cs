using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class AdvertiseController : BaseController
    {
        //
        // GET: /Admin/Advertise/

        [AllowAnonymous]
        public ActionResult Index()
        {
            FillAuthKeys();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Feedback model)
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
                TempData[Constants.ViewBagMessageKey] = "Thank you for your interest in advertising in NITCAA. Your request has been send to NITCAA.";
                return SafeRedirect();
            }
            FillAuthKeys();
            return View(model);
        }

    }
}
