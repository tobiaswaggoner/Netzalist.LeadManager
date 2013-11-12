// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public LogOnUser AuthenticateUser(String username, String password, int tenantId)
        {
            return
                NetzalistDb.Instance.LogOnUsers
                    .Where(
                        item =>
                            item.Name == username && item.Password == password &&
                            item.Tenant.TenantId == tenantId)
                    .Include(u => u.Tenant)
                    .FirstOrDefault();
        }

        public Session CreateSession(LogOnUser user)
        {
            var newSession = new Session {LogOnUser = user, SessionStart = DateTime.Now, SessionEnd = DateTime.Now};
            NetzalistDb.Instance.Sessions.Add(newSession);
            NetzalistDb.Instance.SaveChanges();
            return newSession;
        }

        public void EndSession(Session session)
        {
            session.SessionEnd = DateTime.Now;
            NetzalistDb.Instance.Sessions.Attach(session);
            NetzalistDb.Instance.Entry(session).Property(e => e.SessionEnd).IsModified = true;
            NetzalistDb.Instance.SaveChanges();
        }
    }
}