using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var migrator = new DbMigrator(new Migrations.Configuration());
            var result = migrator.GetPendingMigrations().Aggregate("", (current, nxtMigration) => current + (nxtMigration + " "));
            migrator.Update();
            
            ViewBag.Message = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Substring(0,
                20) + result;


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
