namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTestData : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tenants");
            DropTable("dbo.DeleteMes");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.DeleteMes",
                c => new
                    {
                        DeleteMeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeleteMeId);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        TenantId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                    })
                .PrimaryKey(t => t.TenantId);
            
        }
    }
}
