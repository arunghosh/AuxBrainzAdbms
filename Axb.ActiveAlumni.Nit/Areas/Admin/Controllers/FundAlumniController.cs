using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class FundAlumniController : BaseController
    {
        //
        // GET: /Admin/FundAlumni/

        public ActionResult Index()
        {
            CurrentPage = Infrastructure.PageTypes.SupportAlumni;   
            return View();
        }

    }
}
