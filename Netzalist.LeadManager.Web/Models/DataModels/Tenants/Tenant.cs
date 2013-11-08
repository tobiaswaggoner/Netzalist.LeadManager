// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.Tenants
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}