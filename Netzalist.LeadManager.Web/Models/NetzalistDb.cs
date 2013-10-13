// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models
{
    public class NetzalistDb : DbContext
    {
        public NetzalistDb()
            : base("DefaultConnection")
        {
        }
    }
}