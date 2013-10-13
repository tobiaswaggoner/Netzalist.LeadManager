using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var migrator = new DbMigrator(new Migrations.Configuration());
            var model = new DatabaseMigrationModel {PendingMigrations = new List<string>(), AllMigrations = new List<string>()};

            foreach (var nxtChange in migrator.GetPendingMigrations())
                model.PendingMigrations.Add(nxtChange);

            foreach (var nxtChange in migrator.GetDatabaseMigrations())
                model.AllMigrations.Add(nxtChange);
            return model;
        }

        public void UpdateNow()
        {
            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Update();
            RedirectToAction("Index");
        }

    }
}
