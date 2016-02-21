using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public partial class HomeController : BaseController
    {
        private IAutenticationService _formAuthService = new AutenticationService();
        public HomeController()
        {
            //_formAuthService = formAuthService;
        }

        [AllowAnonymous]
        public ViewResult Ragam(string id)
        {
            var links = new List<RagamLink>
            {
                new RagamLink("about-nit-ragam", "About Ragam"),
                new RagamLink("ragam-tickets-2014", "Buy Ragam Tickets"),
                new RagamLink( "ragam-invite", "Ragam Invitation For Alumni"),
                new RagamLink( "rajan-memorial", "Rajan Memorial Service", Routes.ImageUrl("rajan.jpg")),
                new RagamLink( "sneha-ragam", "Sneha Ragam", Routes.ImageUrl("ragam_s1.jpg")),
                new RagamLink( "ragam-gallery", "Ragam Photo Gallery"),
                
            };
            var link = string.IsNullOrEmpty(id) ? "about-nit-ragam" : id;
            var sele = links.First(l => l.Link == link);
            ViewBag.Link = sele.Link;
            ViewBag.Meta = sele.DisplayText;
            ViewBag.MetaD = "Ragam, the annual inter-college fest conducted by National Institute of Technology, Calicut, is scheduled to be held from March 13-16 this year...";
            ViewBag.OgImg = sele.Photo;
            return View(links);
        }


        [AllowAnonymous]
        public PartialViewResult Sbi()
        {
            return PartialView();
        }


        [AllowAnonymous]
        public PartialViewResult Common()
        {
            FillAuthKeys();
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult CommonMobile()
        {
            FillAuthKeys();
            return PartialView("Common.Mobile");
        }

        [AllowAnonymous]
        public PartialViewResult ProfileBanner()
        {
            var users = DbCache.PicUsers;
            var final = new List<User>();
            var max = users.Count - 1;

            Random rand = new Random(DateTime.Now.Second);
            while (final.Count < 17)
            {
                var user = users[rand.Next(max)];
                if (!final.Contains(user))
                {
                    final.Add(user);
                }
            }
            return PartialView(final);
        }

        [AllowAnonymous]
        //[OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Index(string returnUrl)
        {

            //using (var digestSrv = new DigestService())
            //{
            //    digestSrv.SendDigest(11);
            //}

            CurrentPage = PageTypes.GuestHome;
            if (IsAuth && !CurrentUser.HasSeenTerms)
            {
                TempData[Constants.ViewBagMessageKey] = @"You agree that by registering on <a href='nitcalumni.com' target='_blank'>nitcalumni.com</a>, that you are an alumnus/staff/student of National Institute of Technology, Calicut (NITC / erstwhile  REC Calicut), and the information updated on this site are correct and accurate.You further agree that you will not post any content that is defamatory, obscene or illegal. Copyrighted material, may not be placed on the Site without the permission of the owner of the copyright in the material, or other legal entitlement to use the material. You must not publish any material that would be against the national interest and you take full responsibility of all material that are posted by you. NITC Alumni Association reserves the right to edit or remove any material posted upon our website.";
                var user = _db.Users.Find(CurrentUserId);
                user.HasSeenTerms = true;
                UpdateEntity(user);
            }
            return View();
        }
    }

    public class RagamLink
    {
        public string DisplayText { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }
        public RagamLink(string link, string text, string plink = null)
        {
            Photo = string.IsNullOrEmpty(plink) ? Routes.ImageUrl("invi.jpg") : plink;
            Link = link;
            DisplayText = text;
        }
    }
}
