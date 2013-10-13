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
    public class DeleteMeController : Controller
    {
        private NetzalistDb db = new NetzalistDb();

        //
        // GET: /DeleteMe/

        public ActionResult Index()
        {
            return View(db.DeleteMes.ToList());
        }

        //
        // GET: /DeleteMe/Details/5

        public ActionResult Details(int id = 0)
        {
            DeleteMe deleteme = db.DeleteMes.Find(id);
            if (deleteme == null)
            {
                return HttpNotFound();
            }
            return View(deleteme);
        }

        //
        // GET: /DeleteMe/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DeleteMe/Create

        [HttpPost]
        public ActionResult Create(DeleteMe deleteme)
        {
            if (ModelState.IsValid)
            {
                db.DeleteMes.Add(deleteme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deleteme);
        }

        //
        // GET: /DeleteMe/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DeleteMe deleteme = db.DeleteMes.Find(id);
            if (deleteme == null)
            {
                return HttpNotFound();
            }
            return View(deleteme);
        }

        //
        // POST: /DeleteMe/Edit/5

        [HttpPost]
        public ActionResult Edit(DeleteMe deleteme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deleteme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deleteme);
        }

        //
        // GET: /DeleteMe/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DeleteMe deleteme = db.DeleteMes.Find(id);
            if (deleteme == null)
            {
                return HttpNotFound();
            }
            return View(deleteme);
        }

        //
        // POST: /DeleteMe/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteMe deleteme = db.DeleteMes.Find(id);
            db.DeleteMes.Remove(deleteme);
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