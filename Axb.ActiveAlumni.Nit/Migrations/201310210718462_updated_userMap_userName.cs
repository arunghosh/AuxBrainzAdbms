namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_userMap_userName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageUserMap", "UserId", "dbo.User");
            DropIndex("dbo.MessageUserMap", new[] { "UserId" });
            AddColumn("dbo.MessageUserMap", "UserName", c => c.String(nullable: false, maxLength: 128, defaultValue:"User Name"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageUserMap", "UserName");
            CreateIndex("dbo.MessageUserMap", "UserId");
            AddForeignKey("dbo.MessageUserMap", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
