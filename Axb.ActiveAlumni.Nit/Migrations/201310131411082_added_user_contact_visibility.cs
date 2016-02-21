namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_user_contact_visibility : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "MobileVisibility", c => c.Byte(nullable: false));
            AddColumn("dbo.User", "HomePhoneVisibility", c => c.Byte(nullable: false));
            AddColumn("dbo.User", "EmailVisibility", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "EmailVisibility");
            DropColumn("dbo.User", "HomePhoneVisibility");
            DropColumn("dbo.User", "MobileVisibility");
        }
    }
}
