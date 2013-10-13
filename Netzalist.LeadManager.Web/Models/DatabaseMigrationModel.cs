using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models
{
    public class DatabaseMigrationModel
    {
        public List<Migration> AllMigrations;
        public List<Migration> PendingMigrations;
    }
}