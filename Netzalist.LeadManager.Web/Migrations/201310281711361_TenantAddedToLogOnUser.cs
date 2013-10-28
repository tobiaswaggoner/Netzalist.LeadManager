namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenantAddedToLogOnUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogOnUsers", "Tenant_TenantId", c => c.Int());
            AddForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants", "TenantId");
            CreateIndex("dbo.LogOnUsers", "Tenant_TenantId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LogOnUsers", new[] { "Tenant_TenantId" });
            DropForeignKey("dbo.LogOnUsers", "Tenant_TenantId", "dbo.Tenants");
            DropColumn("dbo.LogOnUsers", "Tenant_TenantId");
        }
    }
}
