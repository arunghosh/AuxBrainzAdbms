using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Axb.ActiveAlumni.Nit.Entities
{
    public partial class AlumniDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> UserSessions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<FailedLogin> FailedLogins { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobOpening> JobOpenings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<ServiceInfo> ServiceInfos { get; set; }
        
        public DbSet<MentorShip> MentorShips { get; set; }
        public DbSet<MentorMessage> MentorMessages { get; set; }

        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<MessageUserMap> MessageUserMaps { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventInvitee> EventInvitees { get; set; }
        public DbSet<EventComment> EventComments { get; set; }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollVote> PollVotes { get; set; }
        public DbSet<PollInvitee> PollInvitees { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }

        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<DiscussionUserMap> DiscussionUserMaps { get; set; }
        public DbSet<DiscussionComment> DiscussionComments { get; set; }
        public DbSet<CommentAffinity> CommentAffinities { get; set; }

        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterHead> ChapterHeads { get; set; }


        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityComment> ActivityComments { get; set; }
        public DbSet<ActivityTask> ActivityTask { get; set; }


        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<AlumniSpeak> AlumnisSpeak { get; set; }
        public DbSet<AlumniToKnow> AlumnisToKnow { get; set; }
        public DbSet<TweetAffinity> TweetAffinities { get; set; }
        public DbSet<AlumniNews> AlumniNewss { get; set; }
        public DbSet<NonAdminNews> NonAdminNewsPosts { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<Circle> Circles { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Institution>()
            //        .HasMany(i => i.Branches).WithMany(c => c.Insitutions)
            //        .Map(t => t.MapLeftKey("InstitutionId")
            //        .MapRightKey("SpecialisationId")
            //        .ToTable("MapInstitutionSpecialisation"));

            //modelBuilder.Entity<User>()
            //        .HasMany(i => i.UserRoles).WithMany(c => c.Users)
            //        .Map(t => t.MapLeftKey("UserId")
            //        .MapRightKey("RoleId")
            //        .ToTable("MapUserRole"));

            modelBuilder.Entity<Circle>()
                    .HasMany(i => i.Members).WithMany(u => u.Circles)
                    .Map(t => t.MapLeftKey("CircleId")
                    .MapRightKey("UserId")
                    .ToTable("MapCircleUser"));

            //modelBuilder.Entity<Circle>()
            //    .HasMany(i => i.Members).WithMany(u => u.Circles)
            //    .Map(t => t.MapLeftKey("CircleId")
            //    .MapRightKey("UserId")
            //    .ToTable("MapCircleUser"));

            modelBuilder.Entity<Institution>()
                    .HasMany(i => i.Branches).WithMany(c => c.Insitutions)
                    .Map(t => t.MapLeftKey("InstitutionId")
                    .MapRightKey("BranchId")
                    .ToTable("MapInstitutionBranch"));
        }
    }
}
