using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Areas.Profile.Models
{
    public class SkillVm: IUserSubProfile
    {
        public string[]  SeleSkills { get; set; }


        public SkillVm()
        {

        }

        public SkillVm(User user)
        {
            SeleSkills = user.GetSkills().ToArray();
        }

        public void UpdateUser(User user)
        {
            user.UpdateSkills(SeleSkills);
        }
    }
}