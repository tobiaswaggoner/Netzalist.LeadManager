namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class personsandcommunicationsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Communications",
                c => new
                    {
                        CommunicationChannelPK = c.Guid(nullable: false),
                        CommunicationType = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CommunicationChannelPK);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonPK = c.Guid(nullable: false),
                        Gender = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                        Birthday = c.DateTime(),
                    })
                .PrimaryKey(t => t.PersonPK);
            
            CreateTable(
                "dbo.PersonCommunications",
                c => new
                    {
                        PersonCommunicationPK = c.Guid(nullable: false),
                        PersonPK = c.Guid(nullable: false),
                        CommunicationPK = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonCommunicationPK)
                .ForeignKey("dbo.Communications", t => t.CommunicationPK, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonPK, cascadeDelete: true)
                .Index(t => t.CommunicationPK)
                .Index(t => t.PersonPK);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PersonCommunications", new[] { "PersonPK" });
            DropIndex("dbo.PersonCommunications", new[] { "CommunicationPK" });
            DropForeignKey("dbo.PersonCommunications", "PersonPK", "dbo.People");
            DropForeignKey("dbo.PersonCommunications", "CommunicationPK", "dbo.Communications");
            DropTable("dbo.PersonCommunications");
            DropTable("dbo.People");
            DropTable("dbo.Communications");
        }
    }
}
