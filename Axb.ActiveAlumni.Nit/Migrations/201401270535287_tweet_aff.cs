namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tweet_aff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TweetAffinity",
                c => new
                    {
                        TweetAffinityId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        IPAddress = c.String(maxLength: 32),
                        Status = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        AlumniToKnowId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TweetAffinityId)
                .ForeignKey("dbo.AlumniToKnow", t => t.AlumniToKnowId, cascadeDelete: true)
                .Index(t => t.AlumniToKnowId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TweetAffinity", new[] { "AlumniToKnowId" });
            DropForeignKey("dbo.TweetAffinity", "AlumniToKnowId", "dbo.AlumniToKnow");
            DropTable("dbo.TweetAffinity");
        }
    }
}
