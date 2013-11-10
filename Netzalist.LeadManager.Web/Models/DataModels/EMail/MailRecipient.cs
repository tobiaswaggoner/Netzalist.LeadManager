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
        [Key]
        [Required]
        public Guid MailRecipientPK { get; set; }

        [Required]
        public Guid MailMessagePK { get; set; }
        [Required]
        public Guid RecipientPK { get; set; }
        [Required]
        public MailRecipientType RecipientType { get; set; }
    }
}