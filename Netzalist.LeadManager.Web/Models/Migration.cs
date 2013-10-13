using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models
{
    public class Migration
    {
        public String MigrationString { get; set; }

        public String Name
        {
            get
            {
                if (String.IsNullOrEmpty(MigrationString)) return MigrationString;
                if (MigrationString.Length < 17) return MigrationString;

                return MigrationString.Substring(16);
            }
        }

        public String Date
        {
            get
            {
                if (String.IsNullOrEmpty(MigrationString)) return "";
                if (MigrationString.Length < 14) return "";

                return MigrationString.Substring(6, 2) + "." +
                       MigrationString.Substring(4, 2) + "." +
                       MigrationString.Substring(0, 4) + " " +
                       MigrationString.Substring(8, 2) + ":" +
                       MigrationString.Substring(10, 2) + ":" +
                       MigrationString.Substring(12, 2);
            }
        }
    }
}