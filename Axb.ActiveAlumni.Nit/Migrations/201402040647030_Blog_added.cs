namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blog_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "DiscusionType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussion", "DiscusionType");
        }
    }
}
