namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates_msg_totally : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageUserMap",
                c => new
                    {
                        MessageUserMapId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MessageThreadId = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageUserMapId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.MessageThread", t => t.MessageThreadId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MessageThreadId);
            
            AddColumn("dbo.MessageThread", "Subject", c => c.String(maxLength: 200));
            DropColumn("dbo.Message", "ReceiverId");
            DropColumn("dbo.Message", "ReceiverName");
            DropColumn("dbo.Message", "IsRead");
            DropColumn("dbo.MessageThread", "SenderId");
            DropColumn("dbo.MessageThread", "ReceiverId");
            DropColumn("dbo.MessageThread", "SenderName");
            DropColumn("dbo.MessageThread", "ReceiverName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageThread", "ReceiverName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MessageThread", "SenderName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MessageThread", "ReceiverId", c => c.Int(nullable: false));
            AddColumn("dbo.MessageThread", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.Message", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Message", "ReceiverName", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Message", "ReceiverId", c => c.Int(nullable: false));
            DropIndex("dbo.MessageUserMap", new[] { "MessageThreadId" });
            DropIndex("dbo.MessageUserMap", new[] { "UserId" });
            DropForeignKey("dbo.MessageUserMap", "MessageThreadId", "dbo.MessageThread");
            DropForeignKey("dbo.MessageUserMap", "UserId", "dbo.User");
            DropColumn("dbo.MessageThread", "Subject");
            DropTable("dbo.MessageUserMap");
        }
    }
}
