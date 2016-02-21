namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_failedloginsd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FailedLogin",
                c => new
                    {
                        FailedLoginId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        IPAddress = c.String(maxLength: 32),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FailedLoginId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FailedLogin");
        }
    }
}
