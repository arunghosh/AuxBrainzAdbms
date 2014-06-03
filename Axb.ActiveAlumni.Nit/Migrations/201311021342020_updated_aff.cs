namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_aff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommentAffinity", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentAffinity", "UserName", c => c.Int(nullable: false));
        }
    }
}
