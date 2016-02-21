namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class crowrd_type_disc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "DiscussCrowdType", c => c.Int(nullable: false, defaultValue:1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussion", "DiscussCrowdType");
        }
    }
}
