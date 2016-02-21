namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_bloodgrp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "BloodGroup", c => c.String(maxLength: 8));
            AddColumn("dbo.User", "CanDonateBlood", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "CanDonateBlood");
            DropColumn("dbo.User", "BloodGroup");
        }
    }
}
