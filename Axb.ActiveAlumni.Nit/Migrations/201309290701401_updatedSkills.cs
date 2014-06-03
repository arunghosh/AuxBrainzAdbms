namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedSkills : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skill", "UserId", "dbo.User");
            DropIndex("dbo.Skill", new[] { "UserId" });
            AddColumn("dbo.User", "Skills", c => c.String());
            DropTable("dbo.Skill");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId);
            
            DropColumn("dbo.User", "Skills");
            CreateIndex("dbo.Skill", "UserId");
            AddForeignKey("dbo.Skill", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
    }
}
