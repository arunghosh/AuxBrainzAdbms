using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public abstract class UserFilter : SearchFilterBase<User, UserSearchTypes>
    {
        public UserFilter(UserSearchTypes type)
            : base(type)
        {

        }
    }

    public class BranchFilter : UserFilter
    {
        public BranchFilter()
            : base(UserSearchTypes.Branch)
        {
            AutoComplete = Routes.AcCourses;
            Name = "Course";
            CkboxName = "_seleCrse";
            IsExpanded = false;
            ShowAll = false;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.UserCourses.Select(c => c.BranchName).Any(b => _checkedItems.Contains(b)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var items = users
                        .SelectMany(u => u.UserCourses.Distinct(new UserCourseComparer()))
                .Select(u => u.BranchName);
            ComposeFilterItems(items);
        }
    }

    public class CourseFilter : UserFilter
    {
        public CourseFilter()
            : base(UserSearchTypes.Course)
        {
            AutoComplete = Routes.AcDegree;
            Name = "Degree";
            CkboxName = "_seleDegree";
            IsExpanded = false;
            ShowAll = true;
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => u.UserCourses.Select(c => c.CourseName).Any(b => _checkedItems.Contains(b)));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var items = users
                        .SelectMany(u => u.UserCourses.Distinct(new UserCourseComparer()))
                .Select(u => u.CourseName);
            ComposeFilterItems(items);
        }
    }

    public class LocationFilter : UserFilter
    {
        public LocationFilter()
            : base(UserSearchTypes.Location)
        {
            AutoComplete = "GetLocations";
            Name = "Location";
            CkboxName = "_seleLocation";
        }

        public override IEnumerable<User> Execute(IEnumerable<User> users)
        {
            if (!_checkedItems.Any()) return users;
            return users.Where(u => _checkedItems.Contains(u.CurrentCity));
        }

        public override void ComposeFilters(IEnumerable<User> users)
        {
            var locations = users.Select(u => u.CurrentCity).ToList();
            locations.RemoveAll(s => string.IsNullOrEmpty(s));
            ComposeFilterItems(locations);
        }
    }

}