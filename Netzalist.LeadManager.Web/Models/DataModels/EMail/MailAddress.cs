// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.EMail
{
    public class MailAddress
    {
        public int MailAddressId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public String Address { get; set; }
        public String DisplayName { get; set; }
        public String Host { get; set; }
        public String User { get; set; }
    }
}