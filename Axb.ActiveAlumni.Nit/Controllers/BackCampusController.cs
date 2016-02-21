using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class BackCampusController : BaseController
    {
        //
        // GET: /BackCampus/

        [AllowAnonymous]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.BackToCampus;
            return View();
        }

    }
}
