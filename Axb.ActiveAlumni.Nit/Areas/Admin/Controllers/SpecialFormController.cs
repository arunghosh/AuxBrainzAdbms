using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    [Authorize]
    public class SpecialFormController : BaseController
    {
        //
        // GET: /Admin/SpecialForm/

        public ActionResult Index()
        {
            var user = CurrentUser;
            if (user.UserCourses.Any(c => c.Batch == "1989"))
            {
                ViewData["isRec89"] = true;
                ViewData["currUserName"] = user.FullName;
            }
            else
            {
                ViewData["isRec89"] = false;
            }
            return View();
        }

    }
}
