using System.Collections.Generic;
using Netzalist.LeadManager.Web.Models.DataModels.Tenants;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public interface IAdministrationService
    {
        List<Tenant> GetAllTenants();
    }
}