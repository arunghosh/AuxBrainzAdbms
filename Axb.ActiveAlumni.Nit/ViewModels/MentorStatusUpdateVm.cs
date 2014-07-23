using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.ViewModels
{

    public class MentorStatusUpdateVm : ServiceBase
    {
        public int Id { get; set; }

        public int AcUserId { get; set; }

        public MentorStatusType Status { get; set; }

        [MentorMsgLength]
        public string Message { get; set; }
    }
}