// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Netzalist.LeadManager.Web.Common;
using Netzalist.LeadManager.Web.DataAccess;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;
using Netzalist.LeadManager.Web.Models.ViewModels.Accounts;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            var model = new LogOnViewModel
            {
                Tenants = ServiceFactory.GetAdministrationService().GetAllTenants()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LogOnViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Bitte Eingaben überprüfen.");
                model.Tenants = ServiceFactory.GetAdministrationService().GetAllTenants();
                return View(model);
            }
            if (Authenticate(model)) return RedirectToLocal(returnUrl);

            ModelState.AddModelError("", "Benutzername oder Passwort sind nicht korrekt.");

            model.Tenants = ServiceFactory.GetAdministrationService().GetAllTenants();
            return View(model);
        }

        [LeadManagerAuthorize]
        public ActionResult LogOut()
        {
            ServiceFactory.GetAuthenticationService().EndSession((Session)Session["Session"]);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private Boolean Authenticate(LogOnViewModel model)
        {
            var user = ServiceFactory.GetAuthenticationService()
                .AuthenticateUser(model.UserName, model.Password, model.SelectedTenant);

            if (user == null)
                return false;

            Session["Session"] = ServiceFactory.GetAuthenticationService().CreateSession(user);

            FormsAuthentication.SetAuthCookie(user.Name, model.RememberMe);
            return true;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}