using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using Axb.ActiveAlumni.Nit.Entities;
using Axb.ActiveAlumni.Nit.Infrastructure;
using Axb.ActiveAlumni.Nit.Services;
using System.Data.Entity;

namespace Axb.ActiveAlumni.Nit.ViewModels
{
    public class DbCache
    {
        //public static readonly DbCache Instance = new DbCache();
        static List<string> _domains = new List<string>
        {
            "Hospital",
            "Banking",
            "Insurance",
            "Automobile",
            "Retail",
        };


        private DbCache()
        {

        }

        static DbCacheItem<User> _userSearch = new DbCacheItem<User>();
        public static List<User> UserSearch
        {
            get
            {
                if (_userSearch.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _userSearch.Items = _db.Users
                                        .Include(u => u.UserRoles)
                                        .Include(u => u.UserCourses)
                                        .Include(u => u.Relatives)
                                        .Where(u => u.UserRoles.Any(r => (byte)r.RoleType < 3) && u.AccountStatus == UserRegisterStatus.Approved)
                                        .ToList();

                    }
                }
                return _userSearch.Items;
            }
        }

        static DbCacheItem<User> _relatives = new DbCacheItem<User>();
        public static List<User> Relatives
        {
            get
            {
                if (_relatives.CanRefresh)
                {
                    var relUser = new List<User>();
                    using (var _db = new AlumniDbContext())
                    {
                        var relatives = UserSearch.SelectMany(u => u.Relatives).ToList();
                        var roles = new List<UserRole> { new UserRole { RoleType = UserRoleType.Relative } };
                        foreach (var relative in relatives)
                        {
                            relative.UserName = relative.User.FullName;
                            var user = new User
                            {
                                FirstName = relative.Name,
                                Jobs = new List<Job>(),
                                Educations = new List<Education>(),
                                UserRoles = roles,
                                UserCourses = new List<UserCourse>(),
                                Relatives = new List<Relative> { relative }
                            };

                            if (!string.IsNullOrEmpty(relative.Work))
                            {
                                user.Jobs.Add(new Job
                                {
                                    CompanyName = relative.Work
                                });
                            }

                            if (!string.IsNullOrEmpty(relative.Education))
                            {
                                user.Educations.Add(new Education
                                {
                                    SchoolName = relative.Education
                                });
                            }
                            relUser.Add(user);
                        }

                    }
                    _relatives.Items = relUser;
                }
                return _relatives.Items;
            }
        }

        static DbCacheItem<User> _profSearch = new DbCacheItem<User>();
        public static List<User> ProfSearch
        {
            get
            {
                if (_profSearch.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _profSearch.Items = _db.Users
                                        .Include(u => u.UserRoles)
                                        .Include(u => u.UserCourses)
                                        .Include(u => u.Jobs)
                                        .Where(u => u.UserRoles.Any(r => (byte)r.RoleType < 3))
                                        .ToList();

                    }
                }
                return _profSearch.Items;
            }
        }

        static DbCacheItem<string> _userNames = new DbCacheItem<string>();
        public static List<string> UserNames
        {
            get
            {
                if (_userNames.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.Users.Select(u => (u.FirstName + " " + u.LastName).Trim()).Distinct().ToList();
                        temp.RemoveAll(u => string.IsNullOrEmpty(u));
                        _userNames.Items = temp;
                    }
                }
                return _userNames.Items;
            }
        }

        static DbCacheItem<User> _autoAdminUsers = new DbCacheItem<User>();
        public static List<User> AutoAdminUsers
        {
            get
            {
                if (_autoAdminUsers.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.Users.Where(u => u.CreateType == UserCreateTypes.Auto || u.CreateType == UserCreateTypes.Admin)
                                            .ToList();
                        _autoAdminUsers.Items = temp;
                    }
                }
                return _autoAdminUsers.Items;
            }
        }

        static DbCacheItem<string> _autoAdminNames = new DbCacheItem<string>();
        public static List<string> AutoAdminNames
        {
            get
            {
                if (_autoAdminNames.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = AutoAdminUsers.Select(u => (u.FirstName + " " + u.LastName).Trim())
                                            .Distinct().ToList();
                        temp.RemoveAll(u => string.IsNullOrEmpty(u));
                        _autoAdminNames.Items = temp;
                    }
                }
                return _autoAdminNames.Items;
            }
        }

        static List<string> _companyNames = null;
        public static List<string> CompanyNames
        {
            get
            {
                if (_companyNames == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _companyNames = _db.Jobs.Select(j => j.CompanyName).Distinct().ToList();
                        var temp = ReadFrom(Routes.CompanyFile);
                        _companyNames = _companyNames.Concat(temp).ToList();
                        _companyNames.RemoveAll(s => string.IsNullOrEmpty(s));
                    }
                }
                return _companyNames;
            }
        }

        static List<Branch> _branches = null;
        public static List<Branch> Branches
        {
            get
            {
                if (_branches == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _branches = _db.Branches.OrderBy(s => s.Name).ToList();
                        _branches = _branches.Distinct().ToList();
                    }
                }
                return _branches;
            }
        }

        static DbCacheItem<User> _picUsers = new DbCacheItem<User>(60);
        public static List<User> PicUsers
        {
            get
            {
                if (_picUsers.CanRefresh)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _picUsers.Items = _db.Users
                                .Include(x => x.Jobs)
                                .Include(x => x.UserCourses)
                                .Where(u => !string.IsNullOrEmpty(u.ImageType) && u.ImageData != null && !u.ImageType.Contains(".pptx") && u.UserRoles.Any(r => r.RoleType == UserRoleType.Alumni))
                                .ToList();
                    }
                }
                return _picUsers.Items;
            }
        }

        static List<Course> _courses = null;
        public static List<Course> Courses
        {
            get
            {
                if (_courses == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _courses = _db.Courses.OrderBy(s => s.Name).ToList();
                    }
                }
                return _courses;
            }
        }


        static List<int> _batches = null;
        public static List<int> Batches
        {
            get
            {
                if (_batches == null)
                {
                    int startDate = 1965;
                    _batches = Enumerable.Range(startDate, DateTime.UtcNow.Year - startDate + 1).ToList();
                }
                return _batches;
            }
        }

        static List<string> _batcheCourse = null;
        public static List<string> BatcheCourse
        {
            get
            {
                if (_batcheCourse == null)
                {
                    var temp = new List<string>();
                    foreach (var batch in Batches)
                    {
                        temp.Add(batch + " #All Batches#");
                        foreach (var course in Branches)
                        {
                            temp.Add(batch + " " + course.Name);
                        }
                    }
                    _batcheCourse = temp;
                }
                return DbCache._batcheCourse;
            }
        }


        static List<UserRole> _roles;
        public static List<UserRole> Roles
        {
            get
            {
                if (_roles == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _roles = _db.UserRoles.ToList();
                    }
                }
                return _roles;
            }
        }

        static List<string> _cities;
        public static List<string> Cities
        {
            get
            {
                if (_cities == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _cities = _db.Users.Select(u => u.CurrentCity).ToList();
                        var preCities = ReadFrom(Routes.CityFile);
                        _cities = _cities.Concat(preCities).Distinct().ToList();
                        _cities.RemoveAll(c => string.IsNullOrEmpty(c));
                    }
                }
                return _cities;
            }
        }

        static List<string> _committePositions;
        public static List<string> CommittePositions
        {
            get
            {
                if (_committePositions == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _committePositions = ReadFrom(Routes.CommittePosFile).ToList();
                        _committePositions.RemoveAll(c => string.IsNullOrEmpty(c));
                    }
                }
                return _committePositions;
            }
        }

        static List<string> _jobPositions;
        public static List<string> JobPositions
        {
            get
            {
                if (_jobPositions == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.Jobs.Select(u => u.Position).Distinct().ToList();
                        var prePos = ReadFrom(Routes.JobPosFile);
                        _jobPositions = temp.Concat(prePos).ToList();
                        _jobPositions.RemoveAll(s => string.IsNullOrEmpty(s));
                    }
                }
                return _jobPositions;
            }
        }

        static List<string> _jobDomains;
        public static List<string> JobDomains
        {
            get
            {
                if (_jobDomains == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.Jobs.Select(u => u.Domain).Distinct().ToList();
                        _jobDomains = temp.Concat(_domains).ToList();
                        _jobDomains.RemoveAll(s => string.IsNullOrEmpty(s));
                    }
                }
                return _jobDomains;
            }
        }

        static List<string> _profileSkills;
        public static List<string> ProfileSkills
        {
            get
            {
                if (_profileSkills == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.Users.Select(u => u.Skills).ToList();
                        temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _profileSkills = temp.SelectMany(t => t.Split('^', ',')).Distinct().ToList();
                    }
                }
                return _profileSkills;
            }
        }


        static List<string> _joinedSkills;
        public static List<string> JoinedSkills
        {
            get
            {
                if (_joinedSkills == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        _joinedSkills = ProfileSkills.Concat(JobSkills).Distinct().ToList();
                    }
                }
                return _joinedSkills;
            }
        }

        static List<string> _jobSkills;
        public static List<string> JobSkills
        {
            get
            {
                if (_jobSkills == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.JobOpenings.Select(u => u.DesiredSkills).ToList();
                        temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _jobSkills = temp.SelectMany(t => t.Split('^',',')).Distinct().ToList();
                    }
                }
                return _jobSkills;
            }
        }


        static List<string> _jobPostOrgs;
        public static List<string> JobPostOrgs
        {
            get
            {
                if (_jobPostOrgs == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.JobOpenings.Select(u => u.Organisation).ToList();
                        temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _jobPostOrgs = temp.Distinct().ToList();
                    }
                }
                return _jobPostOrgs;
            }
        }

        static List<string> _jobPostLocations;
        public static List<string> JobPostLocations
        {
            get
            {
                if (_jobPostLocations == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.JobOpenings.Select(u => u.Location).ToList();
                        temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _jobPostLocations = temp.Distinct().ToList();
                    }
                }
                return _jobPostLocations;
            }
        }
        static List<string> _srvNames;
        public static List<string> SrvNames
        {
            get
            {
                if (_srvNames == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.ServiceInfos.Select(u => u.Title).ToList();
                        //temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _srvNames = temp.Distinct().ToList();
                    }
                }
                return _srvNames;
            }
        }



        static List<string> _srvCtgrys;
        public static List<string> SrvCtgrys
        {
            get
            {
                if (_srvCtgrys == null)
                {
                    using (var _db = new AlumniDbContext())
                    {
                        var temp = _db.ServiceInfos.Select(u => u.Category).ToList();
                        //temp.RemoveAll(s => string.IsNullOrEmpty(s));
                        _srvCtgrys = temp.Distinct().ToList();
                    }
                }
                return _srvCtgrys;
            }
        }

        public static IEnumerable<string> ReadFrom(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        #region Dispose Methods

        bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed ressources
                }
            }

            //dispose unmanaged ressources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}