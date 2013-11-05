namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeTenantRequired : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.LogOnUsers Set Tenant_TenantId = (SELECT TOP 1 TenantId FROM Tenants) WHERE Tenant_TenantId IS NULL ");
            DropForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants");
            DropIndex("dbo.LogOnUsers", new[] { "Tenant_TenantId" });
            AlterColumn("dbo.LogOnUsers", "Tenant_TenantId", c => c.Int(nullable: false));
            AddForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants", "TenantId");
            CreateIndex("dbo.LogOnUsers", "Tenant_TenantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LogOnUsers", new[] { "Tenant_TenantId" });
            DropForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants");
            AlterColumn("dbo.LogOnUsers", "Tenant_TenantId", c => c.Int());
            CreateIndex("dbo.LogOnUsers", "Tenant_TenantId");
            AddForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants", "TenantId");
        }
    }
}
