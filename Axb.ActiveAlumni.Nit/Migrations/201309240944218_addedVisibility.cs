namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVisibility : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "MobileVisbility", c => c.Int(nullable: false));
            AddColumn("dbo.User", "HomePhoneVisibility", c => c.Int(nullable: false));
            AddColumn("dbo.User", "EmailVisibility", c => c.Int(nullable: false));
            AlterColumn("dbo.Relative", "Name", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Relative", "Name", c => c.String(maxLength: 64));
            DropColumn("dbo.User", "EmailVisibility");
            DropColumn("dbo.User", "HomePhoneVisibility");
            DropColumn("dbo.User", "MobileVisbility");
        }
    }
}
