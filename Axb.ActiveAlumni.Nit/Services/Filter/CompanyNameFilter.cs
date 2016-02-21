using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class CompanyNameFilter : UserFilter
    {
        public CompanyNameFilter()
            : base(UserSearchTypes.CompanyName)
        {
            AutoComplete = "GetCompanyNames";
            Name = "Company";
            CkboxName = "_seleCompany";
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.Jobs.Select(j => j.CompanyName).Any(c => _checkedItems.Contains(c)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var jobs = users.SelectMany(u => u.Jobs);
            var companyNames = jobs.Select(j => j.CompanyName);
            ComposeFilterItems(companyNames);
        }
    }

}