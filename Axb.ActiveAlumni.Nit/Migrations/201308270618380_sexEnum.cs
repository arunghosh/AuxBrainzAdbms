namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sexEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Sex", c => c.Byte(nullable: false));
        }
    }
}
