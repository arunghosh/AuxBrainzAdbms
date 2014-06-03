namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_specialOffers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialOffer",
                c => new
                    {
                        SpecialOfferId = c.Int(nullable: false, identity: true),
                        OrganisationName = c.String(nullable: false, maxLength: 256),
                        Address = c.String(maxLength: 512),
                        OfferStatment = c.String(nullable: false, maxLength: 1024),
                        City = c.String(nullable: false, maxLength: 64),
                        Country = c.String(maxLength: 64),
                        Phone = c.String(),
                        Email = c.String(),
                        ImageType = c.String(maxLength: 32),
                        ImageData = c.Binary(),
                        GoogleMap = c.String(maxLength: 1024),
                        CreatedOn = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialOfferId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SpecialOffer", new[] { "UserId" });
            DropForeignKey("dbo.SpecialOffer", "UserId", "dbo.User");
            DropTable("dbo.SpecialOffer");
        }
    }
}
