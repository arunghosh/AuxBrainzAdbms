namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Msgs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageThread",
                c => new
                    {
                        MessageThreadId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        SenderName = c.String(nullable: false, maxLength: 128),
                        ReceiverName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageThreadId);
            
            AddColumn("dbo.Message", "MessageThreadId", c => c.Int(nullable: false));
            AddColumn("dbo.Message", "Text", c => c.String(nullable: false, maxLength: 512));
            AddColumn("dbo.Message", "IsRead", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.Message", "MessageThreadId", "dbo.MessageThread", "MessageThreadId", cascadeDelete: true);
            CreateIndex("dbo.Message", "MessageThreadId");
            DropColumn("dbo.Message", "ReceiverName");
            DropColumn("dbo.Message", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "Content", c => c.String(nullable: false, maxLength: 512));
            AddColumn("dbo.Message", "ReceiverName", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.Message", new[] { "MessageThreadId" });
            DropForeignKey("dbo.Message", "MessageThreadId", "dbo.MessageThread");
            DropColumn("dbo.Message", "IsRead");
            DropColumn("dbo.Message", "Text");
            DropColumn("dbo.Message", "MessageThreadId");
            DropTable("dbo.MessageThread");
        }
    }
}
