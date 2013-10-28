// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Netzalist.LeadManager.Web.Controllers
{
    [LeadManagerAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.Substring(0,
                20);

            return View();
        }

    }
}