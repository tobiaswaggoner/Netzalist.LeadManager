// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Netzalist.LeadManager.Web.Models.Tenants;

namespace Netzalist.LeadManager.Web.Models.Accounts
{
    public class LogOnUser
    {
        public int LogOnUserId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Password { get; set; }

        public Tenant Tenant { get; set; }

    }
}