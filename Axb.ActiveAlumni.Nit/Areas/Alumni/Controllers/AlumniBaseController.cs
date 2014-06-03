using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;

using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Areas.Alumni.Controllers
{
    [AxbAuthorize(Roles = Constants.RoleAlumni)]
    public abstract class AlumniBaseController : BaseController
    {


    }
}
