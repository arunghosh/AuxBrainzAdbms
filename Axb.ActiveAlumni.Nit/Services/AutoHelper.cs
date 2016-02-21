using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public static class AutoHelper
    {
        public static List<string> CompanyNames(string term)
        {
            var names = DbCache.CompanyNames;
            return Filter(names, term);
        }

        public static List<string> CouseNames(string term)
        {
            var names = DbCache.Branches.Select(c => c.Name).ToList();
            return Filter(names, term);
        }

        public static List<string> Degrees(string term)
        {
            var names = DbCache.Courses.Select(c => c.Name).ToList();
            return Filter(names, term);
        }

        public static List<string> UserNames(string term)
        {
            var names = DbCache.UserNames;
            return Filter(names, term);
        }

        public static List<string> AutoNames(string term)
        {
            var names = DbCache.AutoAdminNames;
            return Filter(names, term);
        }

        public static List<string> CityNames(string term)
        {
            var names = DbCache.Cities;
            return Filter(names, term);
        }

        public static List<string> ProfileSkills(string term)
        {
            var names = DbCache.ProfileSkills;
            return Filter(names, term);
        }

        public static List<string> JoinedSkills(string term)
        {
            var names = DbCache.JoinedSkills;
            return Filter(names, term);
        }

        public static List<string> JobSkills(string term)
        {
            var names = DbCache.ProfileSkills;
            return Filter(names, term);
        }

        public static List<string> JobPostLocations(string term)
        {
            var names = DbCache.JobPostLocations;
            return Filter(names, term);
        }
        

        public static List<string> JobPostOrgs(string term)
        {
            var names = DbCache.JobPostOrgs;
            return Filter(names, term);
        }

        public static List<string> JobPostions(string term)
        {
            var names = DbCache.JobPositions;
            return Filter(names, term);
        }

        public static List<string> JobDomains(string term)
        {
            var names = DbCache.JobDomains;
            return Filter(names, term);
        }

        public static List<string> Batches(string term)
        {
            var names = DbCache.Batches;
            return Filter(names, term);
        }

        public static List<string> GetSrvNames(string term)
        {
            var names = DbCache.SrvNames;
            return Filter(names, term);
        }

        public static List<string> GetSrvCtgrys(string term)
        {
            var names = DbCache.SrvCtgrys;
            return Filter(names, term);
        }
        
        private static List<string> Filter(List<string> data, string term)
        {
            term = term.ToLower();
            var filtered = data.Where(n => n.ToLower().Contains(term))
                                .Take(5)
                                .ToList();
            filtered.Sort();
            return filtered;
        }

        private static List<string> Filter(List<int> data, string term)
        {
            term = term.ToLower();
            var filtered = data.Select(d => d.ToString())
                .Where(n => n.Contains(term))
                .Take(5)
                .ToList();
            filtered.Sort();
            return filtered;
        }
    }
}