namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitForeignKeyToCompany : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Companies", name: "CreatedByUser_LogOnUserId", newName: "CreatedByUserId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Companies", name: "CreatedByUserId", newName: "CreatedByUser_LogOnUserId");
        }
    }
}
