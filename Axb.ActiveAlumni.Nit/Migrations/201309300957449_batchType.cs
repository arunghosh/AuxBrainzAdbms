namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class batchType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Batch", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Batch", c => c.Int(nullable: false));
        }
    }
}
