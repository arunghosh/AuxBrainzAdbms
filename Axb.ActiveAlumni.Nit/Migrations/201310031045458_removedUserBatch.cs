namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedUserBatch : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.User", "Batch");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Batch", c => c.String());
        }
    }
}
