namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeSessionEndRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "SessionEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "SessionEnd", c => c.DateTime());
        }
    }
}
