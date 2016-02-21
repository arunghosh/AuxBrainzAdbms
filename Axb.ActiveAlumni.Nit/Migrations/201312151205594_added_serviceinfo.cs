namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_serviceinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceInfo",
                c => new
                    {
                        ServiceInfoId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 128),
                        Category = c.String(maxLength: 64),
                        Description = c.String(maxLength: 1024),
                        CreateOn = c.DateTime(nullable: false),
                        Phone = c.String(maxLength: 32),
                        Email = c.String(maxLength: 64),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceInfoId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ServiceInfo", new[] { "UserId" });
            DropForeignKey("dbo.ServiceInfo", "UserId", "dbo.User");
            DropTable("dbo.ServiceInfo");
        }
    }
}
