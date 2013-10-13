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

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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

        public override void Down()
        {
            DropTable("dbo.Tenants");
        }
    }
}