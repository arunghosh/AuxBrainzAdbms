namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_user_intersts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "MentoringInteset", c => c.Boolean(nullable: false, defaultValue:false));
            AddColumn("dbo.User", "StartupInterest", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.User", "LectureInterest", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.User", "PlacementInterest", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PlacementInterest");
            DropColumn("dbo.User", "LectureInterest");
            DropColumn("dbo.User", "StartupInterest");
            DropColumn("dbo.User", "MentoringInteset");
        }
    }
}
