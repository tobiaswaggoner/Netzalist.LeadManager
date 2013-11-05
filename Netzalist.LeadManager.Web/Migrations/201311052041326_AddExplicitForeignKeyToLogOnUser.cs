namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitForeignKeyToLogOnUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LogOnUsers", name: "Tenant_TenantId", newName: "TenantId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.LogOnUsers", name: "TenantId", newName: "Tenant_TenantId");
        }
    }
}
