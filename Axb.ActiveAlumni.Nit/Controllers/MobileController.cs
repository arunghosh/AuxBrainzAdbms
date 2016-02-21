using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Axb.ActiveAlumni.Nit.Controllers
{

    /// <summary>
    /// This class exposes data required for mobile app in JSON format
    /// </summary>
    public class JsonController : BaseController
    {
        /// <summary>
        /// Get all Events
        /// </summary>
        /// <returns>JSON Array of all events</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult Events()
        {
            var json = new List<dynamic>();
            using (var srv = new EventSrv())
            {
                var past = srv.PastEvents().Take(5);
                var coming = srv.GetUpcoming();
                var events = coming.Concat(past);
                foreach (var e in events)
                {
                    json.Add(new
                    {
                        id = e.EventId,
                        title = e.EventName,
                        text = e.Description,
                        date = e.FromDate
                    });
                }
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Get all News
        /// </summary>
        /// <returns>JSON Array of all News</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult News()
        {
            var json = new List<dynamic>();
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.News).OrderByDescending(n => n.Date).ToList();
            foreach (var e in news.Take(8))
            {
                json.Add(new
                {
                    id = e.AlumniNewsId,
                    title = e.Title,
                });
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get details of a news
        /// </summary>
        /// <returns>JSON of News</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult NewsDetails(int id)
        {
            var news = _db.AlumniNewss.Find(id);
            return Json(news, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all Blogs
        /// </summary>
        /// <returns>JSON Array of all Blogs</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult Blogs()
        {
            var json = new List<dynamic>();
            var news = _db.AlumniNewss.Where(n => n.Status == PostStatusType.Approved && n.NewsType == NewsType.AlumniStory).OrderByDescending(n => n.Date).ToList();
            foreach (var e in news.Take(8))
            {
                json.Add(new
                {
                    id = e.AlumniNewsId,
                    title = e.Title,
                });
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get all Blogs
        /// </summary>
        /// <returns>JSON Array of all Blogs</returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult Tweeks()
        {
            var tweekJson = new List<TweekSummary>();
            foreach (var item in _db.AlumnisToKnow.Where(a => !a.IsDeleted).Take(12).ToList())
            {
                tweekJson.Add(new TweekSummary
                {
                    Id = item.AlumniToKnowId,
                    Tweek = item.About,
                    UserId = item.UserId,
                    UserName = item.User.FullName,
                    Time = item.CreatedOn.ToString("yyyy/MM/dd/HH/mm"),
                    Batch = item.Batch,
                    lCnt = item.AgreeCnt,
                    dCnt = item.DisagreeCnt
                });
            }
            return Json(tweekJson, JsonRequestBehavior.AllowGet);
        }
    }
}
