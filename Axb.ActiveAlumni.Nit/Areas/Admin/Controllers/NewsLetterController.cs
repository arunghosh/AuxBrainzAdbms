using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Areas.Admin.Models;
using Axb.ActiveAlumni.Nit.Controllers;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Controllers
{
    public class NewsLetterController : BaseController
    {
        //
        // GET: /Admin/NewsLetter/

        public ActionResult Index()
        {
            ViewBag.Indexes = new SelectList(Enumerable.Range(0, _db.Users.Count() + 100).Where(u => u % 100 == 0));
            return View(new NewsletterVm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NewsletterVm model)
        {
            var users = _db.Users.Where(u => u.AccountStatus != UserRegisterStatus.Suspended && u.EmailConfirmedOn != null)
                                 .ToList()
                                 .Skip(model.StartIndex)
                                 .Take(model.EndIndex - model.StartIndex).ToList();
            using (var digestSrv = new DigestService())
            {
                var cnt = 1;
                var maxCnt = users.Count;
                foreach (var user in users)
                {
                    digestSrv.SendDigest(user.UserId);
                    Thread.Sleep(900);
                    Debug.WriteLine(cnt++ + "/" + maxCnt);
                }
            }
            var msg = string.Format("Send Newsletters to {0} - {1}. Count: {2}", users.First().UserId, users.Last().UserId, users.Count);
            _userService.AddLog(string.Format("Newsletters Request to {0} - {1}", model.StartIndex, model.EndIndex), LogTypes.Newsletter, "server");
            _userService.AddLog(msg, LogTypes.Newsletter, "server");
            TempData[Constants.ViewBagMessageKey] = msg;
            ViewBag.Indexes = new SelectList(Enumerable.Range(0, _db.Users.Count() + 100).Where(u => u % 100 == 0));
            return View(model);
        }

        public ActionResult Ragam()
        {
            using (TextReader tw = new StreamReader(Routes.WelcomeFile))
            {
                var mail = HtmlComposer.Link("Ragam Promotion Page(NITCAA)", "http://nitcalumni.com/home/ragam");

                mail += HtmlComposer.Div(" ");//("Ragam Offical Webiste", "http://www.ragam.org.in/2014/");
                mail += HtmlComposer.Link("Ragam Official Website", "http://www.ragam.org.in/2014/");
                mail += string.Format("<a href='http://nitcalumni.com/home/ragam'><img src={0} alt='Ragam-2004 Invitation'/></a>", Routes.ImageUrl("invi.jpg"));
                mail += HtmlComposer.Div(" ");//("Ragam Offical Webiste", "http://www.ragam.org.in/2014/");

                var users = _db.Users.Where(u => u.Email == "arunghosh@gmail.com" || u.Email == "rijinjohn@gmail.com" || u.Email == "vivek.george@auxbrains.com").ToList();
                //var users = _db.Users.Where(u => u.AccountStatus != UserRegisterStatus.Suspended).ToList().Skip(6000).Take(1000).ToList();//.Where(u => u.Email == "arunghosh@gmail.com" || u.Email == "rijinjohn@gmail.com" || u.Email == "vivek.george@auxbrains.com").ToList();
                var cnt = 1;
                var maxCnt = users.Count;
                foreach (var user in users)
                {
                    MailSrv.SendMailAsync(user, mail, "Ragam'14 Invitation for NITC Alumni", (s) => { if (!s) { Debug.WriteLine(s); } });
                    Thread.Sleep(900);
                    Debug.WriteLine(cnt++ + "/" + maxCnt);
                }
                return View("AutoWelcome", users.Count);
            }
        }

        public ActionResult AutoWelcome()
        {
            using (TextReader tw = new StreamReader(Routes.WelcomeFile))
            {
                var mail = tw.ReadToEnd();
                //var users = _db.Users.Where(u => u.Email == "arunghosh@gmail.com" || u.Email == "rijinjohn@gmail.com" || u.Email == "vivek.george@auxbrains.com").ToList();
                var users = _db.Users.Where(u => u.AccountStatus != UserRegisterStatus.Suspended)
                                     .ToList()
                                     .Skip(6000)
                                     .Take(1000).ToList();
                var cnt = 1;
                var maxCnt = users.Count;
                foreach (var user in users)
                {
                    MailSrv.SendMailAsync(user, mail, "An Appeal to Alumni to be a part of RAGAM 2014", (s) => { if (!s) { Debug.WriteLine(s); } });
                    Thread.Sleep(800);
                    Debug.WriteLine(cnt++ + "/" + maxCnt);
                }

                return View(users.Count);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Email(string msg)
        {
            //MailSrv.SendMailAsync(new User { Email = "arunghosh@gmail.com" }, msg, "test", null);
            return View();
        }

        [HttpGet]
        public PartialViewResult Email()
        {
            return PartialView();
        }
    }
}
