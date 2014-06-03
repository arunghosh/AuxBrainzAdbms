namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCurrentEJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "IsCurrentEmployer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Job", "IsCurrentEmployer");
        }
    }
}
