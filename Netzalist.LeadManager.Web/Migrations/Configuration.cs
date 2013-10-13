using Netzalist.LeadManager.Web.Models;

namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Netzalist.LeadManager.Web.Models.NetzalistDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Netzalist.LeadManager.Web.Models.NetzalistDb context)
        {
            context.Tenants.AddOrUpdate(i=>i.Name, new Tenant{Name="Autotenant"});
        }
    }
}
