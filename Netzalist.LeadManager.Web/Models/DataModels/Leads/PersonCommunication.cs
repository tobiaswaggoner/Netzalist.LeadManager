// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.Leads
{
    public class PersonCommunication
    {
        [Key]
        [Required]
        public Guid PersonCommunicationPK { get; set; }
        [Required]
        [ForeignKey("Person")]
        public Guid PersonPK { get; set; }
        [Required]
        [ForeignKey("Communication")]
        public Guid CommunicationPK { get; set; }
        [Required]
        public Boolean Active { get; set; }

        public virtual Communication Communication { get; set; }
        public virtual Person Person { get; set; }
    }
}