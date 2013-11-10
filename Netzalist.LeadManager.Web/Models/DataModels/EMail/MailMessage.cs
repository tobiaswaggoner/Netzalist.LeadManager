// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.EMail
{
    public class MailMessage
    {
        [Key]
        [Required]
        public Guid MailMessagePK { get; set; }

        [Required]
        public DateTime DateTimeSent { get; set; }
        [Required]
        public Guid From { get; set; }

        public Guid? Sender { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string ContentType { get; set; }
        public int Encoding { get; set; }

        public MailFlags Flags { get; set; }
        public MailPriority Importance { get; set; }
        public int Size { get; set; }

        [Required]
        public string MessageID { get; set; }
        [Required]
        public string Uid { get; internal set; }
    }
}