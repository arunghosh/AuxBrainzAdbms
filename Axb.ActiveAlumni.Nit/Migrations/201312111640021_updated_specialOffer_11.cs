namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_specialOffer_11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpecialOffer", "UserId", "dbo.User");
            DropIndex("dbo.SpecialOffer", new[] { "UserId" });
            AddColumn("dbo.SpecialOffer", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.SpecialOffer", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.SpecialOffer", "EmailAddress", c => c.String());
            AddColumn("dbo.SpecialOffer", "UserName", c => c.String(maxLength: 128));
            DropColumn("dbo.SpecialOffer", "Email");
            DropColumn("dbo.SpecialOffer", "CreatedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpecialOffer", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.SpecialOffer", "Email", c => c.String());
            DropColumn("dbo.SpecialOffer", "UserName");
            DropColumn("dbo.SpecialOffer", "EmailAddress");
            DropColumn("dbo.SpecialOffer", "Date");
            DropColumn("dbo.SpecialOffer", "Status");
            CreateIndex("dbo.SpecialOffer", "UserId");
            AddForeignKey("dbo.SpecialOffer", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
