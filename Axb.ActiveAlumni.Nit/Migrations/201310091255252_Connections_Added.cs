namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Connections_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Connection",
                c => new
                    {
                        ConnectionId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        Message = c.String(maxLength: 512),
                        SendOn = c.DateTime(nullable: false),
                        RespondedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ConnectionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Connection");
        }
    }
}
