// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Netzalist.LeadManager.Web.Controllers;
using Netzalist.LeadManager.Web.DataAccess;

namespace Netzalist.LeadManager.Web.Tests.Controllers
{
    [TestClass]
    public class CompanyControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var sut = new CompanyController();
            var company = NetzalistDb.Instance.Companies.FirstOrDefault();
            var result = sut.Edit(company.CompanyId);
        }
    }
}