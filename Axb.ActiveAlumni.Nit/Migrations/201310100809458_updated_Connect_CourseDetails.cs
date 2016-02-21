namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Connect_CourseDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connection", "SenderName", c => c.String(maxLength: 128));
            AddColumn("dbo.Connection", "SenderCourse", c => c.String(maxLength: 256));
            AddColumn("dbo.Connection", "Batch", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Connection", "Batch");
            DropColumn("dbo.Connection", "SenderCourse");
            DropColumn("dbo.Connection", "SenderName");
        }
    }
}
