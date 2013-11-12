// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public class LeadService : ILeadService
    {
        public List<Company> GetAllCompanies()
        {
            return NetzalistDb.Instance.Companies.ToList();
        }
    }
}