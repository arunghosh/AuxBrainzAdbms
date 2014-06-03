namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_user_contact_visibility : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "MobileVisbility");
            DropColumn("dbo.User", "HomePhoneVisibility");
            DropColumn("dbo.User", "EmailVisibility");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "EmailVisibility", c => c.Int(nullable: false));
            AddColumn("dbo.User", "HomePhoneVisibility", c => c.Int(nullable: false));
            AddColumn("dbo.User", "MobileVisbility", c => c.Int(nullable: false));
        }
    }
}
