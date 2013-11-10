namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SenderOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailMessages", "Sender", c => c.Guid());
        }
        
        public override void Down()
        {
            Sql("DELETE FROM [MailMessages]");
            AlterColumn("dbo.MailMessages", "Sender", c => c.Guid(nullable: false));
        }
    }
}
