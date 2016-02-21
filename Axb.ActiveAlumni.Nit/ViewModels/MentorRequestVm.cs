using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class MentorRequestVm
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        [MentorMsgLength]
        public string Message { get; set; }

        [Required]
        public string[] SeleSkills { get; set; }

        public bool IsDone { get; set; }

        public MentorRequestVm()
        {
            Message = string.Empty;
            IsDone = false;
        }
    }
}