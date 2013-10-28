namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRememberMe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LogOnUsers", "RememberMe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogOnUsers", "RememberMe", c => c.Boolean(nullable: false));
        }
    }
}
