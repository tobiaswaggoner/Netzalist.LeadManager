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
                Tenants = (from nxtTenant in NetzalistDb.Instance.Tenants select nxtTenant).ToList()
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
                model.Tenants = (from nxtTenant in NetzalistDb.Instance.Tenants select nxtTenant).ToList();
                return View(model);
            }
            if (Authenticate(model)) return RedirectToLocal(returnUrl);

            ModelState.AddModelError("", "Benutzername oder Passwort sind nicht korrekt.");

            model.Tenants = (from nxtTenant in NetzalistDb.Instance.Tenants select nxtTenant).ToList();
            return View(model);
        }

        [LeadManagerAuthorize]
        public ActionResult LogOut()
        {
            var db = NetzalistDb.Instance;

            var session = (Session) Session["Session"];
            session.SessionEnd = DateTime.Now;
            db.Sessions.Attach(session);
            db.Entry(session).Property(e => e.SessionEnd).IsModified = true;
            db.SaveChanges();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private Boolean Authenticate(LogOnViewModel model)
        {
            var db = NetzalistDb.Instance;
            var user =
                db.LogOnUsers
                    .Where(
                        item =>
                            item.Name == model.UserName && item.Password == model.Password &&
                            item.Tenant.TenantId == model.SelectedTenant)
                    .Include(u => u.Tenant)
                    .FirstOrDefault();

            if (user == null)
                return false;

            var newSession = new Session {LogOnUser = user, SessionStart = DateTime.Now, SessionEnd = DateTime.Now};
            db.Sessions.Add(newSession);
            db.SaveChanges();

            Session["Session"] = newSession;

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