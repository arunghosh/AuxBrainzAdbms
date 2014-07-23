namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_poll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Poll",
                c => new
                    {
                        PollId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        IsPublic = c.Boolean(nullable: false),
                        UserGroups = c.Byte(nullable: false),
                        PollType = c.Int(nullable: false),
                        PollTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollId);
            
            CreateTable(
                "dbo.PollInvitee",
                c => new
                    {
                        PollInviteeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 128),
                        Poll_PollId = c.Int(),
                    })
                .PrimaryKey(t => t.PollInviteeId)
                .ForeignKey("dbo.Poll", t => t.Poll_PollId)
                .Index(t => t.Poll_PollId);
            
            CreateTable(
                "dbo.PollOption",
                c => new
                    {
                        PollOptionId = c.Int(nullable: false, identity: true),
                        Text = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        PollId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollOptionId)
                .ForeignKey("dbo.Poll", t => t.PollId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PollId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PollVote",
                c => new
                    {
                        PollVoteId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        IPAddress = c.String(maxLength: 32),
                        TimeStamp = c.DateTime(nullable: false),
                        PollOptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PollVoteId)
                .ForeignKey("dbo.PollOption", t => t.PollOptionId, cascadeDelete: true)
                .Index(t => t.PollOptionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PollVote", new[] { "PollOptionId" });
            DropIndex("dbo.PollOption", new[] { "UserId" });
            DropIndex("dbo.PollOption", new[] { "PollId" });
            DropIndex("dbo.PollInvitee", new[] { "Poll_PollId" });
            DropForeignKey("dbo.PollVote", "PollOptionId", "dbo.PollOption");
            DropForeignKey("dbo.PollOption", "UserId", "dbo.User");
            DropForeignKey("dbo.PollOption", "PollId", "dbo.Poll");
            DropForeignKey("dbo.PollInvitee", "Poll_PollId", "dbo.Poll");
            DropTable("dbo.PollVote");
            DropTable("dbo.PollOption");
            DropTable("dbo.PollInvitee");
            DropTable("dbo.Poll");
        }
    }
}
