// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Netzalist.LeadManager.Web.Models.DataModels.EMail;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public class EmailService : IEmailService
    {
        public MailMessage GetMessage(Guid messagePK)
        {
            return NetzalistDb.Instance.MailMessages.Find(messagePK);
        }
        public MailAddress GetMailAddress(Guid addressPK)
        {
            return NetzalistDb.Instance.MailAddresses.Find(addressPK);
        }

        public List<String> GetListOfMailAddressesForMessage(Guid messagePK, MailRecipientType recipientType)
        {
            return (from MailRecipient rec in NetzalistDb.Instance.MailRecipients
            join MailAddress adr in NetzalistDb.Instance.MailAddresses
                on rec.RecipientPK equals adr.MailAddressPK
            where rec.MailMessagePK == messagePK && rec.RecipientType == recipientType
            select adr.Address).ToList();
        }

        public Boolean MessageExists(String messageId)
        {
            return NetzalistDb.Instance.MailMessages.Any(i => i.MessageID == messageId);
        }

        public void AddMailMessage(MailMessage msg, List<MailRecipient> recipients, List<MailAddress> addresses)
        {
            var db = NetzalistDb.Instance;
            db.MailMessages.Add(msg);
            foreach (var mailAddress in addresses)
                db.MailAddresses.Add(mailAddress);
            foreach (var mailRecipient in recipients)
                db.MailRecipients.Add(mailRecipient);
            db.SaveChanges();
        }

        public MailAddress FindAddressByAddress(String address)
        {
            return NetzalistDb.Instance.MailAddresses.FirstOrDefault(i => i.Address == address.ToLowerInvariant());
        }

    }
}