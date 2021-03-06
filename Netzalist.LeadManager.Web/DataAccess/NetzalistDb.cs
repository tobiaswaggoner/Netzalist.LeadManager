﻿// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;
using Netzalist.LeadManager.Web.Models.DataModels.EMail;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;
using Netzalist.LeadManager.Web.Models.DataModels.Tenants;

namespace Netzalist.LeadManager.Web.DataAccess
{
    public class NetzalistDb : DbContext
    {
        [ThreadStatic] private static NetzalistDb _instance;

        public NetzalistDb()
            : base("DefaultConnection")
        {
        }

        public DbSet<LogOnUser> LogOnUsers { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<MailMessage> MailMessages { get; set; }

        public DbSet<MailAddress> MailAddresses { get; set; }

        public DbSet<MailRecipient> MailRecipients { get; set; }

        public DbSet<Communication> Communications { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<PersonCommunication> PersonCommunications { get; set; }

        public static NetzalistDb Instance
        {
            get
            {
                // *** Create a unique Key for the Web Request/Context 
                String key = null;

                NetzalistDb context;

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

        /// <summary>
        ///     This method is never called. It ensures that the EntityFramework.SqlServer library will be copied to apps using
        ///     this library
        /// </summary>
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            // ReSharper disable once UnusedVariable
            var instance = SqlProviderServices.Instance;
        }
    }
}