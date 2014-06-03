using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class ProfessionalSearchVm:UserSearchBase
    {
        [Display(Name = "Include Relative")]
        public bool IncludeRelative { get; set; }

        public ProfessionalSearchVm()
        {
            FillConnections();
        }

        public override void AddFilters()
        {
            AddFilter(new LocationFilter());
            AddFilter(new CompanyNameFilter
                {
                    IsExpanded = true
                });
            AddFilter(new ConnectFilter());
            AddFilter(new BatchFilter());
            AddFilter(new BranchFilter());
            AddFilter(new SkillFilter());
        }

        public override void FillMasterList()
        {
            IncludeRelative = true;
            MasterList = DbCache.ProfSearch;
            //Add relative details in user
            if (IncludeRelative)
            {
                AddRelatives();
            }
        }

    }
}