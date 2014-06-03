namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_email : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "OptionalEmail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "OptionalEmail", c => c.String(maxLength: 64));
        }
    }
}
