using System;
using System.Collections.Generic;
using Netzalist.LeadManager.Web.Models.DataModels.EMail;

namespace Netzalist.LeadManager.Web.DataAccess.Services
{
    public interface IEmailService
    {
        MailMessage GetMessage(Guid messagePK);
        MailAddress GetMailAddress(Guid addressPK);
        List<String> GetListOfMailAddressesForMessage(Guid messagePK, MailRecipientType recipientType);
        Boolean MessageExists(String messageId);
        void AddMailMessage(MailMessage msg, List<MailRecipient> recipients, List<MailAddress> addresses);
        MailAddress FindAddressByAddress(String address);
    }
}