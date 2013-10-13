namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMeAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeleteMes",
                c => new
                    {
                        DeleteMeId = c.Int(nullable: false, identity: true),
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
