// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Netzalist.LeadManager.Web.Models.DataModels.EMail
{
    public class MailRecipient
    {
        public int MailRecipientId { get; set; }

        [Required]
        public int MailMessageId { get; set; }
        [Required]
        public int RecipientId { get; set; }
        [Required]
        public MailRecipientType RecipientType { get; set; }
    }
}