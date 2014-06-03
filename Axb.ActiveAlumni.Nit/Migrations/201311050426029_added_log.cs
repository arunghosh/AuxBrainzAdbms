namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        LogMessage = c.String(maxLength: 256),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        LogType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Log");
        }
    }
}
