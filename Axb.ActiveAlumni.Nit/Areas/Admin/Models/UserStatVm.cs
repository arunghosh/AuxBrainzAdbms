using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.ViewModels;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.Areas.Admin.Models
{
    public class UserStatVm: UserSearchBase
        {

            [Display(Name = "Include Relative")]
            public bool IncludeRelative { get; set; }

            public UserStatVm()
            {
                FillConnections();
            }

            public override void AddFilters()
            {
                AddFilter(new BatchFilter
                    {
                        ShowAll = true
                    });
                AddFilter(new BranchFilter
                {
                    ShowAll = true
                });
                AddFilter(new CourseFilter
                {
                    ShowAll = true
                });
                AddFilter(new RoleFilter());
            }

            public override void FillMasterList()
            {
                IncludeRelative = true;
                MasterList = _db.Users
                            .Include(u => u.UserRoles)
                            .Include(u => u.UserCourses)
                            .Where(u => u.UserRoles.Any(r => (byte)r.RoleType < 3))
                            .ToList();
            }

        }
}