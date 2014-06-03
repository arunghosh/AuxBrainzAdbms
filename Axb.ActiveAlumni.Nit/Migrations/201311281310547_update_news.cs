namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_news : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlumniNews",
                c => new
                    {
                        AlumniNewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 256),
                        SubTitle = c.String(maxLength: 256),
                        News = c.String(maxLength: 2024),
                        NewsLink = c.String(maxLength: 256),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        NewsType = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlumniNewsId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AlumniNews", new[] { "UserId" });
            DropForeignKey("dbo.AlumniNews", "UserId", "dbo.User");
            DropTable("dbo.AlumniNews");
        }
    }
}
