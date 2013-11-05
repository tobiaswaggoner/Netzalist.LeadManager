// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Netzalist.LeadManager.Web.Models;
using Netzalist.LeadManager.Web.Models.Accounts;
using Netzalist.LeadManager.Web.Models.Tenants;

namespace Netzalist.LeadManager.Web.Migrations
{
    using System;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NetzalistDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NetzalistDb context)
        {
            var tenant = new Tenant {Name = "Default"};
            context.Tenants.AddOrUpdate(t => t.Name, tenant);
            context.LogOnUsers.AddOrUpdate(u=>u.Name, new LogOnUser { Name = "Test", Password = "Test", TenantId = tenant.TenantId});

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}