// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;

namespace Netzalist.LeadManager.Web.Models.ViewModels.Leads
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            Contacts = new List<PersonViewModel>();
        }
        public CompanyViewModel(Company company, List<PersonViewModel> contacts )
        {
            CompanyId = company.CompanyId;
            Name = company.Name;
            Street = company.Street;
            HouseNumber = company.HouseNumber;
            ZipCode = company.ZipCode;
            City = company.City;
            Region = company.Region;
            CountryCode = company.CountryCode;
            Phone = company.Phone;
            Fax = company.Fax;
            Website = company.Website;
            Email = company.Email;
            Contacts = contacts;
        }

        public void SyncDataModel(Company company)
        {
            company.CompanyId = CompanyId;
            company.Name = Name;
            company.Street = Street;
            company.HouseNumber = HouseNumber;
            company.ZipCode = ZipCode;
            company.City = City;
            company.Region = Region;
            company.CountryCode = CountryCode;
            company.Phone = Phone;
            company.Fax = Fax;
            company.Website = Website;
            company.Email = Email;
        }

        public int CompanyId { get; set; }
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

        public List<PersonViewModel> Contacts { get; private set; }

        public PersonViewModel ContactToEdit { get; set; }
    }
}