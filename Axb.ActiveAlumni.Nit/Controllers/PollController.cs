using Axb.ActiveAlumni.Nit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class PollController : BaseController
    {
        //
        // GET: /Poll/

        static readonly object _sync = new object();

        public PartialViewResult Display(int id)
        {
            var poll = _db.Polls.Find(id);
            return PartialView(poll);
        }

        public PartialViewResult AddOptionAndVote(PollOption option)
        {
            if (string.IsNullOrEmpty(option.Text))
            {
                var poll = _db.Polls.Find(option.PollId);
                return PartialView("Display", poll);
            }
            option = UpdateOrAppOption(option);
            return AddVote(option.PollOptionId);
        }

        [HttpPost]
        public PartialViewResult AddVote(int id)
        {
            lock (_sync)
            {
                var user = _db.Users.ToList()[new Random(DateTime.Now.Second).Next(400)];
                var option = _db.PollOptions.Find(id);
                var vote = _db.PollVotes.SingleOrDefault(v => v.UserId == user.UserId && v.Option.PollId == option.PollId);
                if (vote == null)
                {
                    vote = new PollVote
                    {
                        UserId = user.UserId,
                        UserName = user.FullName,
                        PollOptionId = id,
                    };
                    _db.PollVotes.Add(vote);
                }
                else
                {
                    vote.PollOptionId = id;
                    _db.Entry(vote).State = System.Data.EntityState.Modified;
                }
                vote.IPAddress = Request.UserHostAddress;
                _db.SaveChanges();
                var poll = _db.Polls.Find(option.PollId);
                return PartialView("Display", poll);
            }
        }

        public ActionResult Index()
        {
            return View(_db.Polls.ToList());
        }


        public PartialViewResult Edit(int? id, int? eid)
        {
            var model = id == null
                            ? new Poll()
                            : _db.Polls.Find(id);
            if (eid != null)
            {
                model.PollType = PollTypes.Event;
                model.PollTypeId = eid ?? 0;
            }
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(Poll poll)
        {
            if (poll.IsNew)
            {
                _db.Entry(poll).State = System.Data.EntityState.Added;
            }
            else
            {
                _db.Entry(poll).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
            return GetErrorMsgJSON();
        }


        [HttpGet]
        public PartialViewResult EditOption(int? id, int pid)
        {
            var model = id == null
                            ? new PollOption { PollId = pid }
                            : _db.PollOptions.Find(id);
            return PartialView(model);
        }


        [HttpPost]
        public JsonResult EditOption(PollOption option)
        {
            UpdateOrAppOption(option);
            return GetErrorMsgJSON();
        }


        private PollOption UpdateOrAppOption(PollOption option)
        {
            var user = CurrentUser;
            option.UserId = CurrentUser.UserId;
            if (option.IsNew)
            {
                if (_db.PollOptions.Any(p => p.Text == option.Text))
                {
                    return option;
                }
                _db.Entry(option).State = System.Data.EntityState.Added;
            }
            else
            {
                _db.Entry(option).State = System.Data.EntityState.Modified;
            }
            _db.SaveChanges();
            return option;
        }


    }
}
