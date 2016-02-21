namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class affni_update_cnt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DiscussionComment", "Agree");
            DropColumn("dbo.DiscussionComment", "Disagree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DiscussionComment", "Disagree", c => c.Int(nullable: false));
            AddColumn("dbo.DiscussionComment", "Agree", c => c.Int(nullable: false));
        }
    }
}
