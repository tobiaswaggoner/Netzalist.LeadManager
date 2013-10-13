// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.Migrations;
using Netzalist.LeadManager.Web.Models;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class DatabaseMigrationController : Controller
    {
        //
        // GET: /DatabaseMigration/

        public ActionResult Index()
        {
            var model = PopulateModel();
            return View(model);
        }

        private static DatabaseMigrationModel PopulateModel()
        {
            var migrator = new DbMigrator(new Configuration());
            var model = new DatabaseMigrationModel
            {
                PendingMigrations = new List<Migration>(),
                AllMigrations = new List<Migration>()
            };

            foreach (var nxtChange in migrator.GetPendingMigrations())
                model.PendingMigrations.Add(new Migration{MigrationString = nxtChange});

            foreach (var nxtChange in migrator.GetDatabaseMigrations())
                model.AllMigrations.Add(new Migration{MigrationString = nxtChange});
            return model;
        }

        public ActionResult UpdateNow()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateSpecific(String migration)
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update(migration);
            return RedirectToAction("Index");
        }
    }
}