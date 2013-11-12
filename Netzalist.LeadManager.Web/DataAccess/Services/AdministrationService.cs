// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Tenants;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public class AdministrationService : IAdministrationService
    {
        public List<Tenant> GetAllTenants()
        {
            return NetzalistDb.Instance.Tenants.ToList();
        }
    }
}