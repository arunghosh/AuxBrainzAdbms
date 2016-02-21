using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public enum MentorStatusType
    {
        RequestSend = 1,
        AdminApproved = 2,
        AdminRejected = 3,
        AlumniApproved = 4,
        AlumniRejected = 5,
        StudentInfo = 6,
        AlumniInfo = 7,
        AdminInfo = 8,
        SuccessfullyCompleted = 9,
        Terminated = 10,
        Message,
    }
}