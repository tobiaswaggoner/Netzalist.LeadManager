// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.DataAccess.Services;

namespace Netzalist.LeadManager.Web.DataAccess
{
    public class ServiceFactory
    {
        public static Func<IAuthenticationService> GetAuthenticationService =
            () => new AuthenticationService();

        public static Func<IAdministrationService> GetAdministrationService =
            () => new AdministrationService();

        public static Func<IEmailService> GetEmailService =
            () => new EmailService();

        public static Func<ILeadService> GetLeadService =
            () => new LeadService();
    }
}