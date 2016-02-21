using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;

namespace Axb.ActiveAlumni.Nit.Services
{
    public enum CircleSearchTypes
    {
        Name,
        Year
    }

    public abstract class CircleFilter : SearchFilterBase<Circle, CircleSearchTypes>
    {
        public CircleFilter(CircleSearchTypes type)
            : base(type)
        {
        }
    }


    public class CircleNameFilter : CircleFilter
    {
        public override void ComposeFilters(IEnumerable<Circle> items)
        {
            MasterFilters = FilterItems;
            FilterItems = new List<FilterItem>();
            foreach (var item in items)
            {
                var fItem = new FilterItem(_checkedItems.Contains(item.Name))
                {
                    Count = item.Members.Count,
                    ValueText = item.Name,
                    ItemId = item.CircleId,
                    Url = Routes.EditCircle
                };
                FilterItems.Add(fItem);
            }
        }

        public override IEnumerable<Circle> Execute(IEnumerable<Circle> items)
        {
            if (_checkedItems == null || !_checkedItems.Any()) return items;
            var fItems = items.Where(i => _checkedItems.Contains(i.Name));
            return fItems;
        }

        public CircleNameFilter()
            : base(CircleSearchTypes.Name)
        {
            AutoComplete = null;
            Name = "Circles";
            CkboxName = "_seleCircle";
            IsExpanded = true;
            ShowAll = true;
        }
    }
}