using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Services
{
    public enum JobFilterTypes
    {
        Skill,
        Location,
        Company
    }

    public abstract class JobPostFilter : SearchFilterBase<JobOpening, JobFilterTypes>
    {
        public JobPostFilter(JobFilterTypes type)
            : base(type)
        {
            _dispFilterCnt = 5;
        }
    }


    public class JobSkillFilter : JobPostFilter
    {
        public override void ComposeFilters(IEnumerable<JobOpening> items)
        {
            var joinedSkills = items.Select(s => s.DesiredSkills).ToList();
            joinedSkills.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            var skills = joinedSkills.SelectMany(s => s.Split('^'));
            ComposeFilterItems(skills);
        }

        public override IEnumerable<JobOpening> Execute(IEnumerable<JobOpening> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(j => _checkedItems.Any(t => j.DesiredSkills.Contains(t)));
        }

        public JobSkillFilter()
            : base(JobFilterTypes.Skill)
        {
            AutoComplete = Routes.AcJobSkills;
            Name = "Skill";
            CkboxName = "_seleJSkill";
            IsExpanded = true;
        }
    }

    public class JobCompanyFilter : JobPostFilter
    {
        public override void ComposeFilters(IEnumerable<JobOpening> items)
        {
            var orgs = items.Select(s => s.Organisation).ToList();
            orgs.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            ComposeFilterItems(orgs);
        }

        public override IEnumerable<JobOpening> Execute(IEnumerable<JobOpening> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(j => _checkedItems.Any(t => j.Organisation == t));
        }

        public JobCompanyFilter()
            : base(JobFilterTypes.Company)
        {
            AutoComplete = Routes.AcJobPostOrgs;
            Name = "Organisation";
            CkboxName = "_seleJOrg";
            IsExpanded = true;
        }
    }


    public class JobLocationFilter : JobPostFilter
    {
        public override void ComposeFilters(IEnumerable<JobOpening> items)
        {
            var orgs = items.Select(s => s.Location).ToList();
            orgs.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            ComposeFilterItems(orgs);
        }

        public override IEnumerable<JobOpening> Execute(IEnumerable<JobOpening> items)
        {
            if (!_checkedItems.Any()) return items;
            return items.Where(j => _checkedItems.Any(t => j.Location == t));
        }

        public JobLocationFilter()
            : base(JobFilterTypes.Location)
        {
            AutoComplete = Routes.AcJobPostLocations;
            Name = "Location";
            CkboxName = "_seleJLoct";
            IsExpanded = true;
        }
    }

}