namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogOnUserAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogOnUsers",
                c => new
                    {
                        LogOnUserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LogOnUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogOnUsers");
        }
    }
}
