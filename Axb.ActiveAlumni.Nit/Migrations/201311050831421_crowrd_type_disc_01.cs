namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class crowrd_type_disc_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "DiscussionCrowd", c => c.Byte(nullable: false));
            DropColumn("dbo.Discussion", "DiscussCrowdType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discussion", "DiscussCrowdType", c => c.Int(nullable: false));
            DropColumn("dbo.Discussion", "DiscussionCrowd");
        }
    }
}
