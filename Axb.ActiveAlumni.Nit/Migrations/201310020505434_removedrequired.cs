namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCourse", "BranchName", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserCourse", "CourseName", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCourse", "CourseName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserCourse", "BranchName", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
