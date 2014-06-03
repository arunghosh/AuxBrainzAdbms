namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_disc_content_req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Discussion", "Content", c => c.String(nullable: false, defaultValue:"NA"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Discussion", "Content", c => c.String());
        }
    }
}
