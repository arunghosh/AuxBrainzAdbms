using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class SkillProfileController : ProfileControllerBase
    {
        [HttpGet]
        public PartialViewResult Show(int? id)
        {
            var user = GetUserByIdOrCurrent(id);
            var model = new SkillVm(user);
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == user.UserId);
            return PartialView(model);
        }

        [HttpGet]
        public JsonResult List()
        {
            var user = GetCurrentUser();
            var model = new SkillVm(user);
            return Json(model.SeleSkills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult Edit()
        {
            var user = GetCurrentUser();
            var model = new SkillVm(user);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillVm model)
        {
            var result = UpdateUserSubProfile(model);
            return result;
        }

    }
}
