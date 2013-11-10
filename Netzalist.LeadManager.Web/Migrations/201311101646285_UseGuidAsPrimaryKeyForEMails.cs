namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseGuidAsPrimaryKeyForEMails : DbMigration
    {
        public override void Up()
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

            CreateTable(
                "dbo.MailMessages",
                c => new
                {
                    MailMessagePK = c.Guid(nullable: false),
                    DateTimeSent = c.DateTime(nullable: false),
                    From = c.Guid(nullable: false),
                    Sender = c.Guid(nullable: false),
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
                .PrimaryKey(t => t.MailMessagePK);

            CreateTable(
                "dbo.MailRecipients",
                c => new
                {
                    MailRecipientPK = c.Guid(nullable: false),
                    MailMessagePK = c.Guid(nullable: false),
                    RecipientPK = c.Guid(nullable: false),
                    RecipientType = c.Int(nullable: false),

                })
                .PrimaryKey(t => t.MailRecipientPK);

            CreateTable(
                "dbo.MailAddresses",
                c => new
                {
                    MailAddressPK = c.Guid(nullable: false),
                    Address = c.String(nullable: false),
                    DisplayName = c.String(),
                    Host = c.String(),
                    User = c.String(),
                })
                .PrimaryKey(t => t.MailAddressPK);


        }
        


        public override void Down()
        {
            DropTable("dbo.MailRecipients");
            DropTable("dbo.MailAddresses");
            DropTable("dbo.MailMessages");


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
    }
}
