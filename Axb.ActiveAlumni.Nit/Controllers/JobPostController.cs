using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class JobPostController : BaseController
    {

        [HttpGet]
        public PartialViewResult Recent()
        {
            var jobs = _db.JobOpenings.ToList();
            jobs.Reverse();
            return PartialView(jobs.Take(5));
        }

        [HttpGet]
        public ViewResult Search()
        {
            CurrentPage = PageTypes.JobSearch;
            var model = new JobSearchVm();
            model.ApplyFilters();
            return View(model);
        }

        [HttpPost]
        public ViewResult Search(JobSearchVm model)
        {
            model.ApplyFilters(Request.Form);
            return View(model);
        }

        [HttpGet]
        public ViewResult MyPosts(int? id)
        {
            CurrentPage = PageTypes.MyJobPosts;
            var userId = CurrentUserId;
            var jobs = _db.JobOpenings.Where(p => p.UserId == userId).ToList();
            jobs.Reverse();
            ListDisplayVm<JobOpening> model = null;
            if (jobs.Any())
            {
                model = new ListDisplayVm<JobOpening>
                {
                    SelectedId = id ?? jobs.First().JobPostId,
                    Items = jobs
                };
            }
            return View(model);
        }

        [HttpGet]
        public PartialViewResult Show(int id)
        {
            var job = _db.JobOpenings.Find(id);
            return PartialView(job);
        }

        [HttpGet]
        public PartialViewResult Edit(int? id)
        {
            var model = id == null 
                            ? new JobOpening()
                            : _db.JobOpenings.Find(id);
            ViewBag.JobType = model.JobType.ToSelectList();
            var minFill = model.GetMinExperienceFill();
            ViewBag.minExp = new SelectList(minFill, minFill[(model.ExperienceFrom == null) ? 0 : ((int)model.ExperienceFrom + 1)]);

            var maxFill = model.GetMaxExperienceFill();
            ViewBag.maxExp = new SelectList(maxFill, maxFill[(model.ExperienceTo == null) ? 0 : ((int)model.ExperienceTo)]);

            return PartialView(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(JobOpening model, List<string> reqSkill, string minExp, string maxExp)
        {
                var currUser = CurrentUser;
            if (reqSkill == null || !reqSkill.Any())
            {
                ModelState.AddModelError("", "Add atleast one desired skill");
            }
            if (!model.UpdateExperience(minExp, maxExp))
            {
                ModelState.AddModelError("", "Maximum experience should not be less than minimum experience");
            }
            if (model.JobPostId != 0 && model.UserId != currUser.UserId)
            {
                LogUnAuth();
                ModelState.AddModelError("", Strings.UnAuthAccess);
            }
            if (ModelState.IsValid)
            {
                model.UserName = currUser.FullName;
                model.UserId = currUser.UserId;
                model.UpdateSkills(reqSkill);
                if (model.JobPostId == 0)
                {
                    _db.JobOpenings.Add(model);
                }
                else
                {
                    _db.Entry(model).State = System.Data.EntityState.Modified;
                }
                model.UpdatedOn = DateTime.UtcNow;
                _db.SaveChanges();
            }
            return GetErrorMsgJSON();
        }


    }
}
