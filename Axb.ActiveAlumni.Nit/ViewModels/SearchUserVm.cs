using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class SearchUserVm : UserSearchBase
    {
        [Display(Name = "Include Relative")]
        public bool IncludeRelative { get; set; }

        public SearchUserVm()
        {
            FillConnections();
        }

        public override void AddFilters()
        {
            AddFilter(new LocationFilter());
            //AddFilter(new CompanyNameFilter());
            AddFilter(new ConnectFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
            //AddFilter(new SkillFilter());
            //AddFilter(new InterestFilter());
            AddFilter(new NameFilter());
            AddFilter(new CourseFilter());
            AddFilter(new RoleFilter());
        }

        public override void FillMasterList()
        {
            IncludeRelative = true;
            MasterList = DbCache.UserSearch;

            //Add relative details in user
            if (IncludeRelative)
            {
                AddRelatives();
            }
        }

    }
}