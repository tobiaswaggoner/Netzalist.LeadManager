// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.Leads
{
    public class Communication
    {
        [Key]
        [Required]
        public Guid CommunicationChannelPK { get; set; }
        [Required]
        public CommunicationTypes CommunicationType { get; set; }
        [Required][MinLength(2)]
        public String Value { get; set; }
        public String Description { get; set; }
    }
}