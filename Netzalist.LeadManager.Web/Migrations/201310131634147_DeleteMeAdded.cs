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

    public partial class DeleteMeAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeleteMes",
                c => new
                {
                    DeleteMeId = c.Int(false, true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.DeleteMeId);
        }

        public override void Down()
        {
            DropTable("dbo.DeleteMes");
        }
    }
}