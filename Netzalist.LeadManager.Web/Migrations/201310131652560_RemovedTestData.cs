// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Migrations
{
    using System;

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
                    DeleteMeId = c.Int(false, true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.DeleteMeId);

            CreateTable(
                "dbo.Tenants",
                c => new
                {
                    TenantId = c.Int(false, true),
                    Name = c.String(),
                    Street = c.String(),
                })
                .PrimaryKey(t => t.TenantId);
        }
    }
}