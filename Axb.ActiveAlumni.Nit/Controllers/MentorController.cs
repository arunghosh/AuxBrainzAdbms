using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.ViewModels;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class MentorController : BaseController
    {
        MentorSrv _srv = new MentorSrv();

        [HttpGet]
        public PartialViewResult AssignAlumni(int id)
        {
            var mentor = _db.MentorShips.Find(id);
            var model = new MentorStatusUpdateVm{
                Id = id,
                Status = mentor.Status
            };
            return PartialView(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MentorRequest()
        {
            var studentId = CurrentUserId;
            var model = new MentorRequestVm
            {
                StudentId = studentId,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MentorRequest(MentorRequestVm model)
        {
            if (ModelState.IsValid)
            {
                var student = CurrentUser;
                var mentor = new MentorShip
                {
                    StartDate = DateTime.UtcNow,
                    AlumniId = 0,
                    StudentId = model.StudentId,
                    AlumniName = "NA",
                    StudentName = student.FullName,
                };
                var message = new MentorMessage
                {
                    Status = mentor.Status,
                    SenderId = student.UserId,
                    Date = DateTime.UtcNow,
                    Text = model.Message,
                    MentorShipId = mentor.MentorShipId,
                    SenderName = student.FullName
                };
                _db.MentorShips.Add(mentor);
                _db.MentorMessages.Add(message);
                _db.SaveChanges();
                model.IsDone = true;
            }
            return View(model);
        }

        #region Mentor List

        [HttpGet]
        public ActionResult Index(int? id)
        {
            var userId = CurrentUserId;
            CurrentPage = PageTypes.Mentors;
            var mentorList = _srv.MyMentorships;
            if (mentorList.Any())
            {
                var seleId = id ?? mentorList.First().MentorShipId;
                var mentor = mentorList
                    .Single(m => m.MentorShipId == seleId);
                var model = new ListDisplayVm<MentorShip>
                {
                    Items = mentorList,
                    SelectedId = mentor.MentorShipId
                };
                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public PartialViewResult ShowDetails(int id)
        {
            var user = CurrentUser;
            var mentor = _db.MentorShips.Include(x => x.Messages).Single(i => i.MentorShipId == id);
            if (!user.IsAdmin() && mentor.AlumniId != user.UserId && mentor.StudentId != user.UserId)
            {
                LogUnAuth();
                return PartialView(Routes.UnAuthView);
            }
            return PartialView(mentor);
        }

        #endregion

        #region Search Mentoring

        [HttpGet]
        public ActionResult MentorSearch()
        {
            CurrentPage = PageTypes.MentorSearch;
            var model = new SearchMentorVm();
            model.ApplyFilters();
            return View(model);
        }

        [HttpPost]
        public ActionResult MentorSearch(SearchMentorVm model)
        {
            model.ApplyFilters(Request.Form);
            return View(model);
        }

        [HttpGet]
        public ActionResult PendingRequests()
        {
            CurrentPage = PageTypes.MentorSearch;
            var model = new SearchMentorVm();
            model.FilterMap[MentorSearchTypes.Status].CheckedItems.Add(MentorStatusType.RequestSend.ToString());
            model.ApplyFilters();
            return View("MentorSearch", model);
        }

        [HttpGet]
        public PartialViewResult Show(int id)
        {
            var mentor = _db.MentorShips.Include(x => x.Messages).Single(i => i.MentorShipId == id);
            return PartialView(mentor);
        }

        #endregion

        #region Mentor Update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(MentorStatusUpdateVm model)
        {
            UpdateStatusAddMsg(model);
            return GetErrorMsgJSON();
        }

        private MentorShip UpdateStatusAddMsg(MentorStatusUpdateVm model)
        {
            var user = CurrentUser;
            var mentor = _db.MentorShips.Include(m => m.Messages).Single(m => m.MentorShipId == model.Id);
            if (!user.IsAdmin() && mentor.AlumniId != user.UserId && mentor.StudentId != user.UserId)
            {
                LogUnAuth();
                throw new Exception(Strings.UnAuthAccess);
            }
            if (ModelState.IsValid)
            {
                if (mentor.Status != model.Status)
                {
                    if (model.Status == MentorStatusType.AdminApproved)
                    {
                        if (model.AcUserId == 0)
                        {
                            ModelState.AddModelError("", "Select Mentor");
                            return null;
                        }
                        else
                        {
                            var mentorUser = _db.Users.Find(model.AcUserId);
                            mentor.AlumniId = mentorUser.UserId;
                            mentor.AlumniName = mentorUser.FullName;
                            //MailSrv.SendMailAsync(mentorUser, "You have new mentoring request.", "NITCAA - Mentoring Request", null);
                        }
                    }

                    mentor.Status = model.Status == MentorStatusType.Message ? mentor.Status : model.Status;
                    var msg = GetUserMsg(model.Status);
                    var message = new MentorMessage
                    {
                        MentorShipId = mentor.MentorShipId,
                        Status = model.Status,
                        SenderId = CurrentUserId,
                        Date = DateTime.UtcNow,
                        Text = string.IsNullOrWhiteSpace(model.Message) 
                                        ? msg
                                        : model.Message,
                        SenderName = CurrentUser.FullName,
                    };
                    _db.Entry(mentor).State = System.Data.EntityState.Modified;
                    _db.MentorMessages.Add(message);
                    _db.SaveChanges();
                }
            }
            return mentor;
        }

        public static string GetUserMsg(MentorStatusType type)
        {
            switch (type)
            {
                case MentorStatusType.RequestSend:
                    return "Student requested mentoring";
                case MentorStatusType.AdminApproved:
                    return "The mentoring request was approved by Admin and send to Mentor for approval";
                case MentorStatusType.AdminRejected:
                    return "The metoring request was rejected by Admin";
                case MentorStatusType.AlumniApproved:
                    return "The request was approved by Alumni";
                case MentorStatusType.AlumniRejected:
                    return "The request was rejected by Alumni";
                case MentorStatusType.StudentInfo:
                    return "The student is requested to provide more infomation";
                case MentorStatusType.AlumniInfo:
                    return "The request was end to Alumni for more Info";
                case MentorStatusType.AdminInfo:
                    return "The request was send to Admin for more Info";
                case MentorStatusType.SuccessfullyCompleted:
                    return "Successfully Completed";
                case MentorStatusType.Terminated:
                default:
                    return type.ToString();
            }
        }

        #endregion
    }
}
