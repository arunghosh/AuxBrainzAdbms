using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class InterestProfileController : ProfileControllerBase
    {
        //
        // GET: /Profile/InterestProfile/

        [HttpGet]
        public PartialViewResult Show(int? id)
        {
            var user = GetUserByIdOrCurrent(id);
            var model = new IntrestProfileVm(user);
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == user.UserId);
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult Edit()
        {
            var user = GetCurrentUser();
            var model = new IntrestProfileVm(user);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IntrestProfileVm model)
        {
            var result = UpdateUserSubProfile(model);
            return result;
        }
    }
}
