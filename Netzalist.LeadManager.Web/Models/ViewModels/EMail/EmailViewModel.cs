// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.ViewModels.EMail
{
    public class EmailViewModel
    {
        public Guid MailMessagePK { get; set; }
        public DateTime SentDate { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String BodyPreview { get; set; }
        public String CC { get; set; }
        public String BCC { get; set; }
        public String ContentType { get; set; }
    }
}