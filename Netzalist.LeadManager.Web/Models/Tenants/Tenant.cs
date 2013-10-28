using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netzalist.LeadManager.Web.Models.Tenants
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