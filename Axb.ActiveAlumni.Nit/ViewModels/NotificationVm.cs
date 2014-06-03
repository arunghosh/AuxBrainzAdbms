using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class NotificationVm
    {
        public int MentorshipCnt { get; set; }
        public int ConnectReqCnt { get; set; }
        public int MessagesCnt { get; set; }
        public int DiscussionCnt { get; set; }
        public int EventsCnt { get; set; }
        public bool IsDummy { get; set; }

        public NotificationVm()
        {
            IsDummy = false;
        }
    }
}