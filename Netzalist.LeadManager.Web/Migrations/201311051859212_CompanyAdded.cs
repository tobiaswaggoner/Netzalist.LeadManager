namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Region = c.String(),
                        CountryCode = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Website = c.String(),
                        Email = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Tenant_TenantId = c.Int(),
                        CreatedByUser_LogOnUserId = c.Int(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.Tenants", t => t.Tenant_TenantId)
                .ForeignKey("dbo.LogOnUsers", t => t.CreatedByUser_LogOnUserId)
                .Index(t => t.Tenant_TenantId)
                .Index(t => t.CreatedByUser_LogOnUserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Companies", new[] { "CreatedByUser_LogOnUserId" });
            DropIndex("dbo.Companies", new[] { "Tenant_TenantId" });
            DropForeignKey("dbo.Companies", "CreatedByUser_LogOnUserId", "dbo.LogOnUsers");
            DropForeignKey("dbo.Companies", "Tenant_TenantId", "dbo.Tenants");
            DropTable("dbo.Companies");
        }
    }
}
