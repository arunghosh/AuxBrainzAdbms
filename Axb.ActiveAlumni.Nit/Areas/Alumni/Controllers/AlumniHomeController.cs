using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Infrastructure;


namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Controllers
{
    public class AlumniHomeController : AlumniBaseController
    {
        public ActionResult Index()
        {
            CurrentPage = PageTypes.GuestHome;
            return View();
        }
    }
}
