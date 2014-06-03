namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_MsgMap_TimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageUserMap", "LastSendOn", c => c.DateTime(nullable: false, defaultValue: new DateTime(1999, 1, 1)));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageUserMap", "LastSendOn");
        }
    }
}
