namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitForeignKeyToCompany2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "Tenant_TenantId", newName: "TenantId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Companies", name: "TenantId", newName: "Tenant_TenantId");
        }
    }
}
