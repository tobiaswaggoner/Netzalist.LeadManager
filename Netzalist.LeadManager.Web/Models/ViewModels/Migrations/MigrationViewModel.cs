// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.ViewModels.Migrations
{
    public class MigrationViewModel
    {
        public String MigrationString { get; set; }

        public String Name
        {
            get
            {
                if (String.IsNullOrEmpty(MigrationString)) return MigrationString;
                return MigrationString.Length < 17 ? MigrationString : MigrationString.Substring(16);
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