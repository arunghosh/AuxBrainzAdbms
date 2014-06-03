namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_disc_deletion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discussion", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Discussion", "DeletedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Discussion", "DeletedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discussion", "DeletedOn");
            DropColumn("dbo.Discussion", "DeletedBy");
            DropColumn("dbo.Discussion", "IsDeleted");
        }
    }
}
