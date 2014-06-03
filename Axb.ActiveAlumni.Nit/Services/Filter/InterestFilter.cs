using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class InterestFilter : UserFilter
    {

        const string MENTOR_INT = "Project Mentoring";
        const string LECTRE_INT = "Guest Lecture";
        const string PLCMNT_INT = "Placement Guidance";
        const string ENTPRE_INT = "Entrepreneurship";

        Dictionary<string, Func<User, bool>> _intFilter = new Dictionary<string, Func<User, bool>>
        {
            {MENTOR_INT, u => u.MentoringInteset},
            {LECTRE_INT, u => u.LectureInterest},
            {PLCMNT_INT, u => u.PlacementInterest},
            {ENTPRE_INT, u => u.StartupInterest},
        };

        public InterestFilter()
            : base(UserSearchTypes.Interest)
        {
            AutoComplete = null;
            Name = "Interest";
            CkboxName = "_seleInrts";
            IsExpanded = true;
            ShowAll = true;
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            MasterFilters = FilterItems;
            FilterItems = new List<FilterItem>();
            foreach (var item in _intFilter)
            {
                var fItem = new FilterItem(_checkedItems.Contains(item.Key))
                {
                    Count = users.Where(item.Value).Count(),
                    ValueText = item.Key
                };
                FilterItems.Add(fItem);
            }
            MasterFilters = FilterItems;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            foreach (var item in _intFilter)
            {
                if (_checkedItems.Contains(item.Key))
                {
                    users = users.Where(item.Value);
                }
            }
            return users;
        }
    }
}