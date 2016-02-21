namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCourseName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCourse", "BranchName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserCourse", "CourseName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserCourse", "Batch", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCourse", "Batch", c => c.String());
            DropColumn("dbo.UserCourse", "CourseName");
            DropColumn("dbo.UserCourse", "BranchName");
        }
    }
}
