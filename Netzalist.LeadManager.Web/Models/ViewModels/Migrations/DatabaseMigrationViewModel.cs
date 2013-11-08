// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.ViewModels.Migrations
{
    public class DatabaseMigrationViewModel
    {
        public List<MigrationViewModel> AllMigrations;
        public List<MigrationViewModel> PendingMigrations;
    }
}