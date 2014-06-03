using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class JobSearchVm : SearchVmBase<JobOpening, JobPostFilter, JobFilterTypes>
    {
        public JobSearchVm()
        {

        }

        public override void FillMasterList()
        {
            MasterList = _db.JobOpenings.Where(j => j.IsActive).ToList();
            MasterList =  MasterList.Reverse().ToList();
        }

        public override void AddFilters()
        {
            AddFilter(new JobSkillFilter());
            AddFilter(new JobLocationFilter());
            AddFilter(new JobCompanyFilter());
        }
    }
}