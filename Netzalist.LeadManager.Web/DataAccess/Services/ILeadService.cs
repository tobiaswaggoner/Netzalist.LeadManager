using System.Collections.Generic;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public interface ILeadService
    {
        List<Company> GetAllCompanies();
    }
}