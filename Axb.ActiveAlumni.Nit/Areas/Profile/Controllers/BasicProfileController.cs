using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class BasicProfileController : ProfileControllerBase
    {
        //
        // GET: /Profile/BasicProfile/

        [HttpGet]
        public PartialViewResult Show(int? id)
        {
            id = id ?? CurrentUserId;
            var user = _db.Users.Include(u => u.UserRoles).Single(u => u.UserId == id);
            var model = new BasicProfileVm(user);
            ViewData[Constants.RoleKey] = user.RoleStr;
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == user.UserId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit()
        {
            var user = GetCurrentUser();
            var model = new BasicProfileVm(user);
            ViewBag.MaritialStatus = model.MaritialStatus.ToSelectList();
            ViewBag.Sex = model.Sex.ToSelectList();
            ViewBag.BloodGroup = new SelectList(Constants.BloodGroups, model.BloodGroup);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BasicProfileVm model)
        {
            var result = UpdateUserSubProfile(model);
            return result;
        }
    }
}
