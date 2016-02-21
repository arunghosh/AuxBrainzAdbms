namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_edu_batch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Education", "Batch", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Education", "Batch");
        }
    }
}
