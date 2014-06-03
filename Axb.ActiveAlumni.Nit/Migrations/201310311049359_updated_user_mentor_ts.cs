namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_mentor_ts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "TsMentorView", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "TsMentorView");
        }
    }
}
