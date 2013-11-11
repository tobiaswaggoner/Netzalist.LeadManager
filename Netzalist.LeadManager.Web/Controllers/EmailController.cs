using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netzalist.LeadManager.Web.DataAccess;
using Netzalist.LeadManager.Web.Models.DataModels.EMail;
using Netzalist.LeadManager.Web.Models.ViewModels.EMail;

namespace Netzalist.LeadManager.Web.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index(Guid mailMessagePK)
        {
            var message = NetzalistDb.Instance.MailMessages.Find(mailMessagePK);
            var model = CreateEmailViewModelFromMailMessage(message);
            return View(model);
        }
        private EmailViewModel CreateEmailViewModelFromMailMessage(MailMessage nxtMail)
        {
            return new EmailViewModel
            {
                MailMessagePK = nxtMail.MailMessagePK,
                Subject = nxtMail.Subject,
                Body = nxtMail.Body,
                BodyPreview = StripTagsCharArray(nxtMail.Body),
                SentDate = nxtMail.DateTimeSent,
                From = NetzalistDb.Instance.MailAddresses.Find(nxtMail.From).Address,
                To = GetRecipientsList(nxtMail.MailMessagePK, MailRecipientType.To),
                ContentType = nxtMail.ContentType
            };
        }

        private static string StripTagsCharArray(string source)
        {
            var array = new char[source.Length];
            var arrayIndex = 0;
            var inside = false;
            foreach (var @let in source)
            {
                if (@let == '<')
                {
                    inside = true; continue;
                }
                if (@let == '>')
                {
                    inside = false; continue;
                }
                if (inside) continue;

                array[arrayIndex] = @let; arrayIndex++;
            }
            var result = new string(array, 0, arrayIndex).Replace("\r\n", " ");
            if (result.Length > 50)
                return result.Substring(0, 50) + ", [...]";

            return result;

        }

        private String GetRecipientsList(Guid mailMessagePK, MailRecipientType recipientType)
        {
            var list = (
                from MailRecipient rec in NetzalistDb.Instance.MailRecipients
                join Models.DataModels.EMail.MailAddress adr in NetzalistDb.Instance.MailAddresses
                    on rec.RecipientPK equals adr.MailAddressPK
                where rec.MailMessagePK == mailMessagePK && rec.RecipientType == recipientType
                select adr.Address).ToList();
            var result = list.Count > 0 ? list.Aggregate((current, next) => current + "; " + next) : "";
            if (result.Length > 50)
                result = result.Substring(0, 50) + "[...]";
            return result;
        }

    }
}
