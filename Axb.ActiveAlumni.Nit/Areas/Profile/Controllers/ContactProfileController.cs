using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class ContactProfileController : ProfileControllerBase
    {
        //
        // GET: /Profile/ContactProfile/

        [HttpGet]
        public PartialViewResult Show(int? id)
        {
            var currUser = GetCurrentUser();
            var reqUser = GetUserByIdOrCurrent(id);
            var model = new ContactProfileVm(reqUser);
            using (var connect = new ConnectService())
            {
                var myConn = connect.GetConnectReqIds(ConnectStatusType.Accepted);
                if (reqUser.UserId != currUser.UserId && !currUser.IsAdmin())
                {
                    byte code = 0;
                    if (myConn.Contains(currUser.UserId)) code |= (0x01 << (byte)VisibilityType.Connections);
                    if (currUser.IsAlumni()) code |= (0x01 << (byte)VisibilityType.Alumni);
                    if (currUser.IsStudent()) code |= (0x01 << (byte)VisibilityType.Student);
                    if (currUser.IsStaff()) code |= (0x01 << (byte)VisibilityType.Staff);
                    if ((code & reqUser.MobileVisibility) == 0)
                    {
                        model.MobileNumber = "<NA>";
                    }

                    if ((code & reqUser.EmailVisibility) == 0)
                    {
                        model.Email = "<NA>";
                    }

                    if ((code & reqUser.HomePhoneVisibility) == 0)
                    {
                        model.HomePhone = "<NA>";
                    }
                }
            }
            // TODO email visibility
            ViewData[Constants.ProfileEditKey] = (currUser.UserId == reqUser.UserId);
            return PartialView(model);
        }


        [HttpGet]
        public PartialViewResult Edit()
        {
            var user = GetCurrentUser();
            var model = new ContactProfileVm(user);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactProfileVm model)
        {
            JsonResult result;
            try
            {
                if (!string.IsNullOrEmpty(model.Email) && CurrentUser.Email != model.Email)
                {
                    _userService.ValidateEmailUnique(model.Email);
                }

                result = UpdateUserSubProfile(model);
            }
            catch (SimpleException ex)
            {
                ModelState.AddModelError("", ex.Message);
                result = GetErrorMsgJSON();
            }
            return result;
        }

    }
}
