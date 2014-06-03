namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "UserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Address", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            CreateIndex("dbo.Address", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Address", new[] { "UserId" });
            DropForeignKey("dbo.Address", "UserId", "dbo.User");
            DropColumn("dbo.Address", "UserId");
        }
    }
}
