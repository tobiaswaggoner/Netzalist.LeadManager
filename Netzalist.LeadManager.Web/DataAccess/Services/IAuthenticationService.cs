using System;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public interface IAuthenticationService
    {
        LogOnUser AuthenticateUser(String username, String password, int tenantId);
        Session CreateSession(LogOnUser user);
        void EndSession(Session session);
    }
}