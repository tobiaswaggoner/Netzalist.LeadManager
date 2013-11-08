using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.Accounts
{
    public class Session
    {
        public int SessionId { get; set; } 
        [Required]
        public LogOnUser LogOnUser { get; set; }
        [Required]
        public DateTime SessionStart { get; set; }
        [Required]
        public DateTime SessionEnd { get; set; }
    }
}