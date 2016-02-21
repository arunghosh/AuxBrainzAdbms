namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAutoCom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyNameId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Abbrevation = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.CompanyNameId);
            
            CreateTable(
                "dbo.JobPosition",
                c => new
                    {
                        JobPositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Abbrevation = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobPosition");
            DropTable("dbo.Company");
        }
    }
}
