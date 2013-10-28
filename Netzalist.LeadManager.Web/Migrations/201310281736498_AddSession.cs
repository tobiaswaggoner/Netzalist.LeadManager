namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false, identity: true),
                        SessionStart = c.DateTime(nullable: false),
                        SessionEnd = c.DateTime(),
                        LogOnUser_LogOnUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionId)
                .ForeignKey("dbo.LogOnUsers", t => t.LogOnUser_LogOnUserId, cascadeDelete: true)
                .Index(t => t.LogOnUser_LogOnUserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sessions", new[] { "LogOnUser_LogOnUserId" });
            DropForeignKey("dbo.Sessions", "LogOnUser_LogOnUserId", "dbo.LogOnUsers");
            DropTable("dbo.Sessions");
        }
    }
}
