namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseGuidAsPrimaryKeyForMaiilAddress : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MailAddresses", "PK_dbo.MailAddresses");
            DropColumn("dbo.MailAddresses", "MailAddressId");
            AddColumn("dbo.MailAddresses", "MailAddressId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.MailAddresses", "MailAddressId", "PK_dbo.MailAddresses");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MailAddresses", "PK_dbo.MailAddresses");
            DropColumn("dbo.MailAddresses", "MailAddressId");
            AlterColumn("dbo.MailAddresses", "MailAddressId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MailAddresses", "MailAddressId", "PK_dbo.MailAddresses");
        }
    }
}
