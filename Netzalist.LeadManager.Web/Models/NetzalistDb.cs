// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Netzalist.LeadManager.Web.Models.Accounts;
using Netzalist.LeadManager.Web.Models.Tenants;

namespace Netzalist.LeadManager.Web.Models
{
    public class NetzalistDb : DbContext
    {
        public NetzalistDb()
            : base("DefaultConnection")
        {
        }

        public DbSet<LogOnUser> LogOnUsers { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Session> Sessions { get; set; }
    }
}