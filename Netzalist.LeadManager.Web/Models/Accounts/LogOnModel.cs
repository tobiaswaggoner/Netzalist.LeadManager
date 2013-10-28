using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.Models.Tenants;

namespace Netzalist.LeadManager.Web.Models.Accounts
{
    public class LogOnModel
    {
        [Required]
        [Display(Name = "Benutzer Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Display(Name = "Angemeldet bleiben?")]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "Mandant")]
        public int SelectedTenant { get; set; }

        public List<Tenant> Tenants { get; set; } 
    }
}