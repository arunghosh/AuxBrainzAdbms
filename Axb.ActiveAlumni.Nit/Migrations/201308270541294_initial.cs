namespace Axb.ActiveAlumni.Nit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        Abbrevation = c.String(maxLength: 24),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        Abbrevation = c.String(maxLength: 24),
                        StartedOnYear = c.Int(nullable: false),
                        Status = c.Byte(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Institution",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                        City = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Pincode = c.String(maxLength: 256),
                        State = c.String(maxLength: 256),
                        Country = c.String(maxLength: 256),
                        StartYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        Sex = c.Byte(nullable: false),
                        MaritialStatus = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Password = c.String(maxLength: 64),
                        HashedPassword = c.String(maxLength: 256),
                        EmailConfirmationToken = c.String(maxLength: 32),
                        PasswordResetToken = c.String(maxLength: 32),
                        AccountStatus = c.Int(nullable: false),
                        MobileNumber = c.String(maxLength: 32),
                        HomePhone = c.String(maxLength: 32),
                        Email = c.String(nullable: false, maxLength: 64),
                        OptionalEmail = c.String(maxLength: 64),
                        Website = c.String(maxLength: 128),
                        Linkdin = c.String(maxLength: 128),
                        Facebook = c.String(maxLength: 128),
                        BranchId = c.Int(nullable: false),
                        Batch = c.Int(nullable: false),
                        PermanentAddress_AddressId = c.Int(),
                        CurrentAddress_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Address", t => t.PermanentAddress_AddressId)
                .ForeignKey("dbo.Address", t => t.CurrentAddress_AddressId)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.PermanentAddress_AddressId)
                .Index(t => t.CurrentAddress_AddressId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        Name = c.String(maxLength: 32),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(maxLength: 128),
                        City = c.String(maxLength: 64),
                        State = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        Pincode = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.MapInstitutionBranch",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InstitutionId, t.BranchId })
                .ForeignKey("dbo.Institution", t => t.InstitutionId, cascadeDelete: true)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.InstitutionId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.MapUserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MapUserRole", new[] { "RoleId" });
            DropIndex("dbo.MapUserRole", new[] { "UserId" });
            DropIndex("dbo.MapInstitutionBranch", new[] { "BranchId" });
            DropIndex("dbo.MapInstitutionBranch", new[] { "InstitutionId" });
            DropIndex("dbo.User", new[] { "BranchId" });
            DropIndex("dbo.User", new[] { "CurrentAddress_AddressId" });
            DropIndex("dbo.User", new[] { "PermanentAddress_AddressId" });
            DropIndex("dbo.Branch", new[] { "CourseId" });
            DropForeignKey("dbo.MapUserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.MapUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.MapInstitutionBranch", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.MapInstitutionBranch", "InstitutionId", "dbo.Institution");
            DropForeignKey("dbo.User", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.User", "CurrentAddress_AddressId", "dbo.Address");
            DropForeignKey("dbo.User", "PermanentAddress_AddressId", "dbo.Address");
            DropForeignKey("dbo.Branch", "CourseId", "dbo.Course");
            DropTable("dbo.MapUserRole");
            DropTable("dbo.MapInstitutionBranch");
            DropTable("dbo.Address");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Institution");
            DropTable("dbo.Branch");
            DropTable("dbo.Course");
        }
    }
}
