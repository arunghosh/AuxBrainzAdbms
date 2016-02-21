using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class MessageController : BaseController
    {
        MessageSrv _service = new MessageSrv();

        [HttpGet]
        public ViewResult Index(int? id)
        {
            var userId = CurrentUserId;
            var threads = _service.MyMesssageThreads;
            if (threads.Any())
            {
                var dispItems = _service.ComposeDispItems(threads);
                var selectedId = id ?? dispItems.First().ItemId;
                var model = new ListDisplayVm<NotifyDispItem>
                {
                    Items = dispItems,
                    SelectedId = selectedId
                };
                return View(model);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public PartialViewResult Show(int id)
        {
            var model = _db.MessageThreads
                                .Include(m => m.Messages)
                                .Include(m => m.MessageUserMaps)
                                .Single(m => m.MessageThreadId == id);
            var userId = CurrentUserId;
            if (!model.MessageUserMaps.Any(m => m.UserId == userId))
            {
                LogUnAuth();
                return PartialView(Routes.UnAuthView);
            }
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult New(List<int> userIds)
        {
            userIds  = userIds == null ? new List<int>() : userIds;
            userIds.Remove(CurrentUserId);
            var users = _db.Users.Where(u => userIds.Contains(u.UserId)).ToList();
            return PartialView(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ExecuteMessageMany(NewMessageVm model)
        {
            if (model.AcSeleUserIds == null || !model.AcSeleUserIds.Any())
            {
                ModelState.AddModelError("", "Specify at least one recipient");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.MessageMany(model.Message, model.AcSeleUserIds);
                }
                catch
                {
                    ModelState.AddModelError("", "Failed to send message");
                }
            }
            return GetErrorMsgJSON();
        }

        [HttpGet]
        public PartialViewResult Popup(int id)
        {
            var user = _db.Users.Find(id);
            var model = new NewMessageVm
            {
                ReceiverId = id,
                ReceiverName = user.FullName
            };
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult NewMessage(NewMessageVm model)
        {
            if (ModelState.IsValid)
            {
                _service.MessageOne(model.Message, model.ReceiverId);
            }
            return GetErrorMsgJSON();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddMessage(NewMessageVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.AddMessageToThread(model.Message, model.ThreadId);
                }
                catch (UnauthorisedExcpetion)
                {
                    LogUnAuth();
                }
            }
            return GetErrorMsgJSON();
        }
    }
}
