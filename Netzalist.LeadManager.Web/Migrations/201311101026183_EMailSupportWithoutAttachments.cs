namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EMailSupportWithoutAttachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailMessages",
                c => new
                    {
                        MailMessageId = c.Int(nullable: false, identity: true),
                        DateTimeSent = c.DateTime(nullable: false),
                        From = c.Int(nullable: false),
                        Sender = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        ContentType = c.String(),
                        Encoding = c.Int(nullable: false),
                        Flags = c.Int(nullable: false),
                        Importance = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        MessageID = c.String(nullable: false),
                        Uid = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MailMessageId);
            
            CreateTable(
                "dbo.MailRecipients",
                c => new
                    {
                        MailRecipientId = c.Int(nullable: false, identity: true),
                        MailMessageId = c.Int(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        RecipientType = c.Int(nullable: false),
                        MailMessage_MailMessageId = c.Int(),
                        MailMessage_MailMessageId1 = c.Int(),
                        MailMessage_MailMessageId2 = c.Int(),
                    })
                .PrimaryKey(t => t.MailRecipientId)
                .ForeignKey("dbo.MailMessages", t => t.MailMessage_MailMessageId)
                .ForeignKey("dbo.MailMessages", t => t.MailMessage_MailMessageId1)
                .ForeignKey("dbo.MailMessages", t => t.MailMessage_MailMessageId2)
                .Index(t => t.MailMessage_MailMessageId)
                .Index(t => t.MailMessage_MailMessageId1)
                .Index(t => t.MailMessage_MailMessageId2);
            
            CreateTable(
                "dbo.MailAddresses",
                c => new
                    {
                        MailAddressId = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                        DisplayName = c.String(),
                        Host = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.MailAddressId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MailRecipients", new[] { "MailMessage_MailMessageId2" });
            DropIndex("dbo.MailRecipients", new[] { "MailMessage_MailMessageId1" });
            DropIndex("dbo.MailRecipients", new[] { "MailMessage_MailMessageId" });
            DropForeignKey("dbo.MailRecipients", "MailMessage_MailMessageId2", "dbo.MailMessages");
            DropForeignKey("dbo.MailRecipients", "MailMessage_MailMessageId1", "dbo.MailMessages");
            DropForeignKey("dbo.MailRecipients", "MailMessage_MailMessageId", "dbo.MailMessages");
            DropTable("dbo.MailAddresses");
            DropTable("dbo.MailRecipients");
            DropTable("dbo.MailMessages");
        }
    }
}
