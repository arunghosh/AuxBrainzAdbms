using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class AlumniOrgController : BaseController
    {
        //
        // GET: /AlumniOrg/

        [AllowAnonymous]
        public ActionResult Chapters()
        {
            CurrentPage = PageTypes.Chapters;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Parent()
        {
            CurrentPage = PageTypes.ParentChapter;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Charter()
        {
            CurrentPage = PageTypes.Charter;
            return View();
        }

    }
}
