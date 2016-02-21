using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class SkillFilter : UserFilter
    {
        public SkillFilter()
            : base(UserSearchTypes.Skill)
        {
            AutoComplete = Routes.AcProfileSkills;
            Name = "Skill";
            CkboxName = "_selePSkill";
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => _checkedItems.Any(t => u.Skills.Contains(t)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var joinedSkills = users.Select(u => u.Skills).ToList();
            joinedSkills.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            var skills = joinedSkills.SelectMany(s => s.Split('^', ','));
            ComposeFilterItems(skills);
        }
    }
}