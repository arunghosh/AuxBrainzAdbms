namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_poll_option : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PollOption", "Text", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PollOption", "Text", c => c.Int(nullable: false));
        }
    }
}
