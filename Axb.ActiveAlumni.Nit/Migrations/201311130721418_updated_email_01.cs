namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_email_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "OptionalEmail", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "OptionalEmail", c => c.String());
        }
    }
}
