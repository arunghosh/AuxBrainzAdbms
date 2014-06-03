namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Axb.ActiveAlumni.Nit.Entities;
    

    internal sealed class Configuration : DbMigrationsConfiguration<Axb.ActiveAlumni.Nit.Entities.AlumniDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Axb.ActiveAlumni.Nit.Entities.AlumniDbContext context)
        {

            //context.Institutions.AddOrUpdate(i => i.Name, new Institution { Name = "NIT", City = "Calicut", StartYear = 1970 });
            //context.SaveChanges();

            if (!context.Courses.Any())
            {
                // Add Courses
                context.Courses.AddOrUpdate(c => c.Name, new Course { Name = "B.Tech" });
                context.Courses.AddOrUpdate(c => c.Name, new Course { Name = "M.Tech" });
                context.Courses.AddOrUpdate(c => c.Name, new Course { Name = "MBA" });
                context.SaveChanges();

                var branches = new List<string>
                {
                    "Electrical & Electronics Engineering",
"Chemical Engineering",
"Civil Engineering",
"Engineering Physics",
"Mechanical Engineering",
"Production Engineering",
"Electronics & Communication",
"Biotechnology",
"Computer Science & Engineering",
                };

                foreach (var item in branches)
                {
                    context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = item, CourseId = 1 });
                }

                // Add Branches
                //context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = "IT", CourseId = 1 });
                //context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = "CSE", CourseId = 1 });
                //context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = "Algorith", CourseId = 2 });
                context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = "M.Tech", CourseId = 2 });
                context.Branches.AddOrUpdate(c => c.Name, new Branch { Name = "MBA", CourseId = 3 });
                context.SaveChanges();
            }
        }
    }
}
