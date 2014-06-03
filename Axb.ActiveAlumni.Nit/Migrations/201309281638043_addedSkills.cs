namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSkills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Skill", new[] { "UserId" });
            DropForeignKey("dbo.Skill", "UserId", "dbo.User");
            DropTable("dbo.Skill");
        }
    }
}
