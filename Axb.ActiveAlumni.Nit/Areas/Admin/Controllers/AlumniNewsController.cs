using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class AlumniNewsController : BaseController
    {

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult SearchNews(string term)
        {
            term = term.ToLower();
            var news = _db.AlumniNewss.Where(n => n.NewsType == NewsType.News && n.Status == PostStatusType.Approved).ToList();
            news = news.Where(n => n.Title.ToLower().Contains(term)).ToList();
            return PartialView(news);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult BlogIndex(int? id)
        {
            return CommonIndex(id, NewsType.AlumniStory);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            return CommonIndex(id, NewsType.News);
        }

        private ViewResult CommonIndex(int? id, NewsType type)
        {
            CurrentPage = PageTypes.News;
            ListDisplayVm<AlumniNews> model = null;
            FillAuthKeys();
            var list = (bool)ViewData[Constants.IsAdminKey]
                            ? _db.AlumniNewss.Where(n => n.NewsType == type).ToList()
                            : _db.AlumniNewss.Where(n => n.NewsType == type && n.Status == PostStatusType.Approved).ToList();
            list.Reverse();
            if (list.Any())
            {
                model = new ListDisplayVm<AlumniNews>
                {
                    SelectedId = id ?? list.First().AlumniNewsId,
                    Items = list
                };

                var sele = list.Find(l => l.AlumniNewsId == model.SelectedId);
                if (sele != null)
                {
                    ViewBag.Meta = sele.Title;
                    ViewBag.MetaD = sele.NewsType == NewsType.AlumniStory ? sele.SubTitle : sele.News.LetterLimited(80);
                    ViewBag.OgImg = Routes.NewsImg(model.SelectedId);
                    //if (sele.NewsType == NewsType.AlumniStory)
                    //{
                    //    ViewBag.Meta = "Alumni Story : " + sele.Title;
                    //    ViewBag.MetaD = sele.SubTitle;
                    //    ViewBag.OgImg = Routes.ImageUrl("alumni_speak.png");
                    //}
                    //else
                    //{
                    //    ViewBag.OgImg = Routes.NewsImg(model.SelectedId);
                    //}
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Show(int id)
        {
            FillAuthKeys();
            var article = _db.AlumniNewss.Find(id);
            if (article.NewsType != NewsType.News)
            {
                return PartialView("BlogShow", article);
            }
            return PartialView(article);
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult Summary()
        {
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.News).OrderByDescending(n => n.Date).ToList();
            return PartialView(news.Take(4));
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult BlogSummary()
        {
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.AlumniStory).OrderByDescending(n => n.Date).ToList();
            return PartialView(news.Take(4));
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public PartialViewResult BlogSummary()
        //{
        //    var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.AlumniStory).OrderByDescending(n => n.Date).ToList();
        //    return PartialView(news.Take(4));
        //}

        [AllowAnonymous]
        public ActionResult NewsImage(int id)
        {
            if (id != 0)
            {
                var offer = _db.AlumniNewss.Find(id);
                if (offer.ImageData != null && offer.ImageData.Length > 0)
                {
                    return new ImageResult(offer.ImageData, offer.ImageType);
                    //var imgRslt = ImageSrv.Thumbnail(Image.FromStream(new MemoryStream(offer.ImageData)), 200, 100);
                    //return imgRslt;
                }
            }
            return GetDefaultImg();
        }

        [AllowAnonymous]
        public ActionResult NewsThumb(int id)
        {
            if (id != 0)
            {
                var offer = _db.AlumniNewss.Find(id);
                if (offer.ImageData != null && offer.ImageData.Length > 0)
                {
                    var imgRslt = ImageSrv.ThumbnailCrop(Image.FromStream(new MemoryStream(offer.ImageData)), 70, 70);
                    return imgRslt;
                }
            }
            return GetDefaultImg();
        }

        private ActionResult GetDefaultImg()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~\Content\images\newsfb.jpg");
            return ImageSrv.Thumbnail(Image.FromFile(path), 100, 100);
        }

        #region Edit

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Edit(int? id)
        {
            var news = new AlumniNews();
            if (id != null && IsAuth)
            {
                news = _db.AlumniNewss.Find(id);
            }
            ViewBag.Status = news.Status.ToSelectList();
            FillAuthKeys();
            return View(news);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit(AlumniNews model)
        {
            return EditCommon(model);
        }

        [HttpGet]
        public ViewResult BlogEdit(int? id)
        {
            var model = id == null ? new AlumniNews { NewsType = NewsType.Blog } : _db.AlumniNewss.Find(id);
            FillAuthKeys();
            ViewBag.Status = model.Status.ToSelectList();
            ViewBag.NewsType = model.NewsType.ToSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogEdit(AlumniNews model)
        {
            return EditCommon(model);
        }

        private ActionResult EditCommon(AlumniNews model)
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
                    TempData[Constants.ViewBagMessageKey] = Strings.NewsPostSuccess;
                    _db.Entry(model).State = System.Data.EntityState.Added;
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }

                _db.SaveChanges();
                return SafeRedirect();

            }
            FillAuthKeys();
            ViewBag.Status = model.Status.ToSelectList();
            ViewBag.NewsType = model.NewsType.ToSelectList();
            return View(model);
        }

        #endregion
    }
}
