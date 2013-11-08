// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;
using Netzalist.LeadManager.Web.Models.DataModels.Tenants;

namespace Netzalist.LeadManager.Web.Models.DataModels.Leads
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public Tenant Tenant { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        public String Name { get; set; }

        public String Street { get; set; }
        public String HouseNumber { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String CountryCode { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        public String Website { get; set; }
        public String Email { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("CreatedByUser")]
        [Required]
        public int CreatedByUserId { get; set; }

        public LogOnUser CreatedByUser { get; set; }
    }
}