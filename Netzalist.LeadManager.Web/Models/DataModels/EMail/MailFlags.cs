// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.EMail
{
    [Flags]
    public enum MailFlags
    {
        None = 0,
        Seen = 1,
        Answered = 2,
        Flagged = 4,
        Deleted = 8,
        Draft = 16,
    }
}