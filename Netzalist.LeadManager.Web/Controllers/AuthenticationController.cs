using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Netzalist.LeadManager.Web.Models;
using Netzalist.LeadManager.Web.Models.Accounts;
using WebMatrix.WebData;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {

            var model = new LogOnModel
            {
                Tenants = (from nxtTenant in new NetzalistDb().Tenants select nxtTenant).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Bitte Eingaben überprüfen.");
                model.Tenants = (from nxtTenant in new NetzalistDb().Tenants select nxtTenant).ToList();
                return View(model);
            }
            if (Authenticate(model)) return RedirectToLocal(returnUrl);

            ModelState.AddModelError("", "Benutzername oder Passwort sind nicht korrekt.");

            model.Tenants = (from nxtTenant in new NetzalistDb().Tenants select nxtTenant).ToList();
            return View(model);
        }

        [LeadManagerAuthorize]
        public ActionResult LogOut()
        {
            var db = new NetzalistDb();

            var session = (Session)Session["Session"];
            session.SessionEnd = DateTime.Now;
            db.Sessions.Attach(session);
            var entry = db.Entry(session);
            entry.Property(e => e.SessionEnd).IsModified = true;
            db.SaveChanges();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private Boolean Authenticate(LogOnModel model)
        {
            var db = new NetzalistDb(); 
            var user =
                db.LogOnUsers.FirstOrDefault(item => item.Name == model.UserName && item.Password == model.Password && item.Tenant.TenantId == model.SelectedTenant);

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
