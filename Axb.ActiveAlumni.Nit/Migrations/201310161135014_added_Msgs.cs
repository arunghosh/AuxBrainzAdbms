namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Msgs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        SenderName = c.String(nullable: false, maxLength: 128),
                        ReceiverName = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.MessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Message");
        }
    }
}
