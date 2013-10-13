using Netzalist.LeadManager.Web.Models;

namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Netzalist.LeadManager.Web.Models.DBTest>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Netzalist.LeadManager.Web.Models.DBTest context)
        {
            context.Tests.AddOrUpdate(i=>i.Name, new Wow{Name="Testname"});
        }
    }
}
