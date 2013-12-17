// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;

namespace Netzalist.LeadManager.Web.Models.ViewModels.Leads
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {
            Communications = new List<Communication>();
        }

        public PersonViewModel(Person person, IEnumerable<Communication> communications )
        {
            PersonPK = person.PersonPK;
            Gender = person.Gender;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Birthday = person.Birthday;
            Communications = new List<Communication>();
            Communications.AddRange(communications);
        }

        public Guid PersonPK { get; set; }
        public Gender Gender { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime? Birthday { get; set; }

        public List<Communication> Communications { get; private set; } 
    }
}