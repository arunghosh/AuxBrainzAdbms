using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Controllers
{
    public class NotificationController : BaseController
    {

        const string POP_VIEW = "NotifyPopup";
        MessageSrv _msgSrv = new MessageSrv();
        DiscussionSrv _discussionSrv = new DiscussionSrv();
        MentorSrv _mentorSrv = new MentorSrv();
        EventSrv _evetnSrv = new EventSrv();
        ConnectService _connectSrv = new ConnectService();

        const int MAX_COUNT = 10;

        public PartialViewResult Index()
        {
            var model = new NotificationVm
            {
                MentorshipCnt = _mentorSrv.NewItemsCount,
                ConnectReqCnt = _connectSrv.GetNewConnectReq().Count,
                MessagesCnt = _msgSrv.NewItemsCount,
                DiscussionCnt = _discussionSrv.NewItemsCount,
                EventsCnt = _evetnSrv.NewItemsCount,
            };
            return PartialView(model);
        }

        public PartialViewResult Dummy()
        {
            return PartialView("Index", new NotificationVm
                {
                    IsDummy = true
                });
        }

        [HttpGet]
        [AxbAuthorize]
        public PartialViewResult Messages()
        {
            var model = _msgSrv.GetNotifyModel();
            _msgSrv.MarkAsNotified();
            return PartialView(POP_VIEW, model);
        }

        [HttpGet]
        public PartialViewResult Event()
        {
            var model = _evetnSrv.GetNotifyModel();
            _evetnSrv.MarkAsNotified();
            return PartialView(POP_VIEW, model);
        }

        [HttpGet]
        public PartialViewResult Discussion()
        {
            var model = _discussionSrv.GetNotifyModel();
            _discussionSrv.MarkAsNotified();
            return PartialView(POP_VIEW, model);
        }

        [HttpGet]
        public PartialViewResult Connect()
        {
            var connect = new ConnectService();
            return PartialView(connect.GetNewConnectReq());
        }

        [HttpGet]
        public PartialViewResult Mentor()
        {
            var model = _mentorSrv.GetNotifyModel();
            _mentorSrv.MarkAsNotified();
            return PartialView(POP_VIEW, model);
        }
    }
}
