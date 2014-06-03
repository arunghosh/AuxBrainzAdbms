using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.ComponentModel.DataAnnotations;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class AdminSearchVm : UserSearchBase
    {

        [Display(Name = "Include Relative")]
        public bool IncludeRelative { get; set; }

        public AdminSearchVm()
        {
            FillConnections();
        }

        public override void FillMasterList()
        {
            IncludeRelative = true;
            MasterList = DbCache.UserSearch;
        }

        public override void AddFilters()
        {
            AddFilter(new InterestFilter());
            AddFilter(new CourseFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
        }
    }
}