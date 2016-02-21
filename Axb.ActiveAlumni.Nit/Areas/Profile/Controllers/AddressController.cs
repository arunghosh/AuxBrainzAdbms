using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Profile.Models;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Controllers
{
    public class AddressController : ProfileControllerBase
    {
        [HttpGet]
        public PartialViewResult List(int? id)
        {
            int userId = id ?? CurrentUserId;
            return PartialView(userId);
        }

        [HttpGet]
        public PartialViewResult Permanent(int? id)
        {
            int userId = id ?? CurrentUserId;
            var address = GetAddress(userId, AddressType.Permanent);
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == userId);
            return PartialView("Show", address);
        }

        [HttpGet]
        public PartialViewResult Current(int? id)
        {
            int userId = id ?? CurrentUserId;
            var address = GetAddress(userId, AddressType.Current);
            ViewData[Constants.ProfileEditKey] = (CurrentUserId == userId);
            return PartialView("Show", address);
        }

        [HttpGet]
        public PartialViewResult PermanentEdit(int? id)
        {
            int userId = id ?? CurrentUserId;
            var address = GetAddress(userId, AddressType.Permanent);
            return PartialView("Edit", address);
        }

        [HttpGet]
        public PartialViewResult CurrentEdit(int? id)
        {
            int userId = id ?? CurrentUserId;
            var address = GetAddress(userId, AddressType.Current);
            return PartialView("Edit", address);
        }

        [NonAction]
        private Address GetAddress(int userId, AddressType type)
        {
            var address = _db.Addresses.FirstOrDefault(a => a.UserId == userId && a.AddressType == type);
            if (address == null)
            {
                address = new Address
                {
                    AddressType = type,
                    AddressId = 0,
                    UserId = userId,
                };
            }
            return address;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Address address)
        {
            var result = UpdateUserOwnedEntity(address);
            if (ModelState.IsValid)
            {
                var user = GetCurrentUser();
                if (address.AddressType == AddressType.Current)
                {
                    user.CurrentCity = address.City;
                    user.CurrentCountry = address.Country;
                    user.CurrentAddress = address;
                }
                else
                {
                    user.PermanentAddress = address;
                }
                UpdateEntity(user);
            }
            return result;
        }

    }
}
