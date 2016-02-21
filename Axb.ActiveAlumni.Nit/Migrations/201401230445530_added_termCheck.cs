namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_termCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "HasSeenTerms", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "HasSeenTerms");
        }
    }
}
