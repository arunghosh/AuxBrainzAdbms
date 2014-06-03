namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNameMsg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MentorMessage", "UserName", c => c.String(nullable: false, maxLength: 128, defaultValue:"User Name"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MentorMessage", "UserName");
        }
    }
}
