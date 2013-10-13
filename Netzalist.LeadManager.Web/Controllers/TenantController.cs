using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.Models;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class TenantController : Controller
    {
        private NetzalistDb db = new NetzalistDb();

        //
        // GET: /Tenant/

        public ActionResult Index()
        {
            return View(db.Tenants.ToList());
        }

        //
        // GET: /Tenant/Details/5

        public ActionResult Details(int id = 0)
        {
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        //
        // GET: /Tenant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tenant/Create

        [HttpPost]
        public ActionResult Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Tenants.Add(tenant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tenant);
        }

        //
        // GET: /Tenant/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        //
        // POST: /Tenant/Edit/5

        [HttpPost]
        public ActionResult Edit(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tenant);
        }

        //
        // GET: /Tenant/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        //
        // POST: /Tenant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}