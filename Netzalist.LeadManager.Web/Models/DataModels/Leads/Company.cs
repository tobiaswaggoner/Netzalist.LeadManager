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
        [Display(Name = "Name")]
        public String Name { get; set; }
        [Display(Name = "Straße")]
        public String Street { get; set; }
        [Display(Name = "HNr")]
        public String HouseNumber { get; set; }
        [Display(Name = "PLZ")]
        public String ZipCode { get; set; }
        [Display(Name = "Ort")]
        public String City { get; set; }
        [Display(Name = "Region")]
        public String Region { get; set; }
        [Display(Name = "Land")]
        public String CountryCode { get; set; }
        [Display(Name = "Telefon")]
        public String Phone { get; set; }
        [Display(Name = "Fax")]
        public String Fax { get; set; }
        [Display(Name = "Website")]
        public String Website { get; set; }
        [Display(Name = "EMail")]
        public String Email { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("CreatedByUser")]
        [Required]
        public int CreatedByUserId { get; set; }

        public LogOnUser CreatedByUser { get; set; }
    }
}