using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class ProfileIdVm
    {
        public List<int> AcceptedIds { get; set; }
        public List<int> PendingIds { get; set; }
        public List<int> RejectedIds { get; set; }

        public User ReqUser { get; set; }
        public int CurrUserId { get; set; }
        public int ReqUserId { get; set; }
        public bool CanReqMentor { get; set; }
        public int? UserId { get; set; }
        public bool IsCurrAdmin { get; set; }
        public ProfileIdVm()
        {
            var connect = new ConnectService();
            PendingIds = connect.GetConnectReqIds(ConnectStatusType.RequestSend);
            AcceptedIds = connect.GetConnectReqIds(ConnectStatusType.Accepted);
            RejectedIds = connect.GetConnectReqIds(ConnectStatusType.Rejected);
            PendingIds = RejectedIds.Concat(PendingIds).ToList();

        }
    }
}