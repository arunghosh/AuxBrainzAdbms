using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class RegisterSearchVm : SearchVmBase<User, UserFilter, UserSearchTypes>
    {
        public RegisterSearchVm()
        {

        }

        public override void FillMasterList()
        {
            MasterList = _db.Users
                .Include(u => u.UserCourses).ToList();
        }

        public override void AddFilters()
        {
            AddFilter(new EmailVerifyFilter());
            AddFilter(new RegisterStatusFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
            AddFilter(new CourseFilter());
        }
    }
}