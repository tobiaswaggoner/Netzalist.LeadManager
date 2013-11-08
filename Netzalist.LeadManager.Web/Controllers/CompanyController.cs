// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.Common;
using Netzalist.LeadManager.Web.DataAccess;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;

namespace Netzalist.LeadManager.Web.Controllers
{
    [LeadManagerAuthorize]
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View(NetzalistDb.Instance.Companies.ToList());
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            ModelState.Clear();
            var session = (Session) Session["Session"];
            company.CreatedByUserId = session.LogOnUser.LogOnUserId;
            company.TenantId = session.LogOnUser.Tenant.TenantId;
            company.CreationDate = DateTime.Now;

            TryValidateModel(company);
            if (!ModelState.IsValid) return View(company);
            var db = NetzalistDb.Instance;
            db.Companies.Add(company);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                var db = NetzalistDb.Instance;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var db = NetzalistDb.Instance;
            var company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}