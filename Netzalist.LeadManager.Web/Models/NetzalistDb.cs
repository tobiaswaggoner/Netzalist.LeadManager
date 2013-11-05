// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using Netzalist.LeadManager.Web.Models.Accounts;
using Netzalist.LeadManager.Web.Models.Leads;
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

        public DbSet<Company> Companies { get; set; }


        [ThreadStaticAttribute] 
        private static NetzalistDb _instance;

        public static NetzalistDb Instance
        {
            get
            {
                // *** Create a unique Key for the Web Request/Context 
                String key = null;

                NetzalistDb context = null;

                if (HttpContext.Current != null)
                {
                    key = "__WRSCDC_" + HttpContext.Current.GetHashCode().ToString("x");
                    context = (NetzalistDb) HttpContext.Current.Items[key];
                }
                else
                    context = _instance;

                return context ?? CreateNewContext(key);
            }
        }

        private static NetzalistDb CreateNewContext(string key)
        {
            var context = new NetzalistDb();
            context.Configuration.LazyLoadingEnabled = false;

            if (HttpContext.Current != null)
                HttpContext.Current.Items[key] = context;
            else
                _instance = context;

            return context;
        }
    }
}