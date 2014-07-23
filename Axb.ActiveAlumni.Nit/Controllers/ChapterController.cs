using Axb.ActiveAlumni.Nit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class ChapterController : BaseController
    {
        public PartialViewResult EditHead(int? id, int cid)
        {
            var model = id == null ? new ChapterHead() : _db.ChapterHeads.Find(id);
            model.ChapterId = cid;
            ViewBag.Positions = new SelectList(DbCache.CommittePositions);
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditHead(ChapterHead model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    _db.ChapterHeads.Add(model);
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }

                User user;

                if (model.AcSeleUserIds != null && model.AcSeleUserIds.Any())
                {
                    user = _db.Users.Find(model.AcSeleUserIds[0]);
                }
                else
                {
                    user = _db.Users.Find(model.UserId);
                }
                model.SetUser(user);

                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult ChapterNews(int id)
        {
            var chapter = _db.Chapters.Find(id);
            var news = _db.AlumniNewss.Where(n => n.NewsType == NewsType.News && n.Status == PostStatusType.Approved).ToList();
            IEnumerable<AlumniNews> grpNews = new List<AlumniNews>();
            foreach (var item in chapter.Alias.Split(','))
            {
                grpNews = grpNews.Concat(news.Where(n => n.Title.ToLower().Contains(item)));
            }
            grpNews = grpNews.Distinct();
            return PartialView(grpNews);
        }



        public PartialViewResult Edit(int? id)
        {
            var model = id == null ? new Chapter() : _db.Chapters.Find(id);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Chapter model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    _db.Chapters.Add(model);
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = model.Name;
                }
                model.Alias = model.Alias.ToLower();
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = _db.Chapters.ToList();
            FillAuthKeys();
            return View(model);
        }

        [AllowAnonymous]
        public PartialViewResult Details()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult Users(int id)
        {
            var chapter = _db.Chapters.Find(id);
            ViewBag.ChapterId = id;
            FillAuthKeys();
            return PartialView(chapter.Users.OrderBy(c => c.FullName).ToList());
        }
    }
}
