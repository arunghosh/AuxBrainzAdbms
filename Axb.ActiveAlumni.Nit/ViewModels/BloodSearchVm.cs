using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class BloodSearchVm : UserSearchBase
    {

        public BloodSearchVm()
            :base()
        {
            FillConnections();
        }

        public override void AddFilters()
        {
            AddFilter(new BloodFilter());
            AddFilter(new LocationFilter());
            AddFilter(new ConnectFilter());
        }

        public override void FillMasterList()
        {
            MasterList = _db.Users
                        .Include(u => u.UserCourses)
                        .Where(u => !string.IsNullOrEmpty(u.BloodGroup) && u.CanDonateBlood)
                        .ToList();
        }
    }

}