using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class BatchFilter : UserFilter
    {
        public BatchFilter()
            : base(UserSearchTypes.Batch)
        {
            AutoComplete = "GetBatches";
            Name = "Batch";
            CkboxName = "_seleBatch";
            IsExpanded = true;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.UserCourses.Select(c => c.Batch).Any(b => _checkedItems.Contains(b)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var batches = users
                        .SelectMany(u => u.UserCourses.Distinct(new UserCourseComparer()))
                        .Select(u => u.Batch);
            var currUser = users.SingleOrDefault(u => u.UserId == UserSession.CurrentUserId);
            if (currUser != null && currUser.UserCourses.Any())
            {
                var tempShowAll = ShowAll;
                ShowAll = true;
                ComposeFilterItems(batches);
                var batch = currUser.UserCourses[0].Batch;
                var bactchInt = int.Parse(batch);
                var batch3 = (bactchInt + 2).ToString();
                var batch1 = (bactchInt + 1).ToString();
                var batch2 = (bactchInt - 1).ToString();
                FilterItems = FilterItems
                                .OrderByDescending(f => f.ValueText == batch2)
                                .OrderByDescending(f => f.ValueText == batch3)
                                .OrderByDescending(f => f.ValueText == batch1)
                                .OrderByDescending(f => f.ValueText == batch)
                                .ToList();

                if (!tempShowAll)
                {
                    var finalquery = FilterItems.Take(_dispFilterCnt);
                    var checkedSkiped = FilterItems.Skip(_dispFilterCnt)
                        .Where(l => l.IsChecked);
                    finalquery = finalquery.Concat(checkedSkiped);
                    FilterItems = finalquery.ToList();
                }

                ShowAll = tempShowAll;
                MasterFilters = FilterItems;
            }
            else
            {
                ComposeFilterItems(batches);
            }
        }
    }

    class UserCourseComparer : IEqualityComparer<UserCourse>
    {
        public bool Equals(UserCourse x, UserCourse y)
        {
            return x.Batch == y.Batch;
        }

        public int GetHashCode(UserCourse obj)
        {
            return int.Parse(obj.Batch);
        }
    }
}