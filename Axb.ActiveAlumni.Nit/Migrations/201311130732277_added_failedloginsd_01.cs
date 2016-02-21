namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_failedloginsd_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FailedLogin", "Message", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FailedLogin", "Message");
        }
    }
}
