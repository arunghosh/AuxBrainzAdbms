using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class SpecialOfferController : BaseController
    {
        //
        // GET: /Admin/SpecialOffer/

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Summary()
        {
            var offers = _db.SpecialOffers.Where(o => o.Status == PostStatusType.Approved).ToList();
            offers.Reverse();
            return PartialView(offers.Take(4));
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Show(int id = 0)
        {
            var offers = _db.SpecialOffers.Where(o => o.Status == PostStatusType.Approved).ToList();
            offers.Reverse();
            var pos = id % offers.Count();
            return PartialView(offers[id % 3]);
        }

        [AllowAnonymous]
        public ActionResult OfferImageSmall(int id)
        {
            if (id != 0)
            {
                var offer = _db.SpecialOffers.Find(id);
                if (offer.ImageData != null && offer.ImageData.Length > 0)
                {
                    var imgRslt = ImageSrv.Thumbnail(Image.FromStream(new MemoryStream(offer.ImageData)), 220/2, 150/2);
                    return imgRslt;
                }
            }
            return GetDefaultImg();
        }

        [AllowAnonymous]
        public ActionResult OfferImage(int id)
        {
            if (id != 0)
            {
                var offer = _db.SpecialOffers.Find(id);
                if (offer.ImageData != null && offer.ImageData.Length > 0)
                {
                    var imgRslt = ImageSrv.Thumbnail(Image.FromStream(new MemoryStream(offer.ImageData)), 220, 150);
                    return imgRslt;
                }
            }
            return GetDefaultImg();
        }

        private ActionResult GetDefaultImg()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~\Content\images\no_img.png");
            return ImageSrv.Thumbnail(Image.FromFile(path), 100, 100);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            CurrentPage = PageTypes.SpecialOffers;
            ListDisplayVm<SpecialOffer> model = null;
            FillAuthKeys();
            var offers = (bool)ViewData[Constants.IsAdminKey]
                            ? _db.SpecialOffers.ToList()
                            : _db.SpecialOffers.Where(o => o.Status == PostStatusType.Approved).ToList();
            if (offers.Any())
            {
                model = new ListDisplayVm<SpecialOffer>
                {
                    SelectedId = offers.First().SpecialOfferId,
                    Items = offers.ToList()
                };
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            var offer = new SpecialOffer();
            if (id != null)
            {
                offer = _db.SpecialOffers.Find(id);
            }
            FillAuthKeys();
            ViewBag.Status = offer.Status.ToSelectList();
            return View(offer);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpecialOffer model)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 700 * 1024)
                {
                    ModelState.AddModelError("", "The maximum allowed picture size is 600KB");
                }
            }

            if (ModelState.IsValid)
            {
                if (IsAuth)
                {
                    var currUser = CurrentUser;
                    model.UserId = currUser.UserId;
                    model.UserName = currUser.FullName;
                    model.Status = currUser.IsAdmin() ? model.Status : PostStatusType.Pending;
                }
                else
                {
                    model.Status = PostStatusType.Pending;
                }

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    byte[] imageBytes = new byte[file.ContentLength];
                    file.InputStream.Read(imageBytes, 0, (int)file.ContentLength);
                    model.ImageType = file.FileName.Split('\\').Last();
                    model.ImageData = imageBytes;
                }

                if (model.EntityKey == 0)
                {
                    TempData[Constants.ViewBagMessageKey] = Strings.OfferPostSuccess;
                    _db.Entry(model).State = System.Data.EntityState.Added;
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            FillAuthKeys();
            ViewBag.Status = model.Status.ToSelectList();
            return View(model);
        }
    }
}
//try
//{
//}
//catch (DbEntityValidationException e)
//{
//    foreach (var eve in e.EntityValidationErrors)
//    {
//        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//            eve.Entry.Entity.GetType().Name, eve.Entry.State);
//        foreach (var ve in eve.ValidationErrors)
//        {
//            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                ve.PropertyName, ve.ErrorMessage);
//        }
//    }
//    throw;
//}