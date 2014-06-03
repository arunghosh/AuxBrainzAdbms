using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class SearchMentorVm : SearchVmBase<MentorShip, MentorFilter, MentorSearchTypes>
    {
        public SearchMentorVm()
        {
        }

        public override void FillMasterList()
        {
            MasterList = _db.MentorShips
                .Include(i => i.Messages)
                .ToList()
                .OrderByDescending(m => m.StartDate);
        }

        public override void AddFilters()
        {
            AddFilter(new MentorStatusFilter());
        }
    }
}