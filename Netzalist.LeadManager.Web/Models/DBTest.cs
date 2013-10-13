using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models
{
    public class DBTest : DbContext
    {
        public DbSet<Test> Tests { get; set; }
    }
}