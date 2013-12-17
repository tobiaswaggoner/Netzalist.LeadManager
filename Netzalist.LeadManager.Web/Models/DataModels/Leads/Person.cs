// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.Leads
{
    public class Person
    {
        [Key]
        [Required]
        public Guid PersonPK { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public String FirstName { get; set; }
        [Required][MinLength(2)]
        public String LastName { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<PersonCommunication> Communications { get; set; }
    }
}