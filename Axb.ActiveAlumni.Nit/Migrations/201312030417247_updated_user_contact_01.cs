namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_user_contact_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "MobileConfirmationToken", c => c.String(maxLength: 10));
            AddColumn("dbo.User", "NotifyViaEmail", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "NotifyViaMobile", c => c.Boolean(nullable: false));
            AddColumn("dbo.User", "MobileConfirmedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "MobileConfirmedOn");
            DropColumn("dbo.User", "NotifyViaMobile");
            DropColumn("dbo.User", "NotifyViaEmail");
            DropColumn("dbo.User", "MobileConfirmationToken");
        }
    }
}
