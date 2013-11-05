namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeSomeFieldsRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Companies", "Tenant_TenantId", "dbo.Tenants");
            DropForeignKey("dbo.Companies", "CreatedByUser_LogOnUserId", "dbo.LogOnUsers");
            DropIndex("dbo.Companies", new[] { "Tenant_TenantId" });
            DropIndex("dbo.Companies", new[] { "CreatedByUser_LogOnUserId" });
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Tenant_TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.Companies", "CreatedByUser_LogOnUserId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Companies", "Tenant_TenantId", "dbo.Tenants", "TenantId", cascadeDelete: true);
            AddForeignKey("dbo.Companies", "CreatedByUser_LogOnUserId", "dbo.LogOnUsers", "LogOnUserId", cascadeDelete: true);
            CreateIndex("dbo.Companies", "Tenant_TenantId");
            CreateIndex("dbo.Companies", "CreatedByUser_LogOnUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Companies", new[] { "CreatedByUser_LogOnUserId" });
            DropIndex("dbo.Companies", new[] { "Tenant_TenantId" });
            DropForeignKey("dbo.Companies", "CreatedByUser_LogOnUserId", "dbo.LogOnUsers");
            DropForeignKey("dbo.Companies", "Tenant_TenantId", "dbo.Tenants");
            AlterColumn("dbo.Companies", "CreatedByUser_LogOnUserId", c => c.Int());
            AlterColumn("dbo.Companies", "Tenant_TenantId", c => c.Int());
            AlterColumn("dbo.Companies", "Name", c => c.String());
            CreateIndex("dbo.Companies", "CreatedByUser_LogOnUserId");
            CreateIndex("dbo.Companies", "Tenant_TenantId");
            AddForeignKey("dbo.Companies", "CreatedByUser_LogOnUserId", "dbo.LogOnUsers", "LogOnUserId");
            AddForeignKey("dbo.Companies", "Tenant_TenantId", "dbo.Tenants", "TenantId");
        }
    }
}
