// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AE.Net.Mail;
using Netzalist.LeadManager.Web.Common;
using Netzalist.LeadManager.Web.DataAccess;
using Netzalist.LeadManager.Web.Models.DataModels.Accounts;
using Netzalist.LeadManager.Web.Models.DataModels.EMail;
using Netzalist.LeadManager.Web.Models.DataModels.Leads;
using Netzalist.LeadManager.Web.Models.ViewModels.EMail;
using MailAddress = System.Net.Mail.MailAddress;
using MailMessage = AE.Net.Mail.MailMessage;
using MailPriority = Netzalist.LeadManager.Web.Models.DataModels.EMail.MailPriority;

namespace Netzalist.LeadManager.Web.Controllers
{
    [LeadManagerAuthorize]
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View(NetzalistDb.Instance.Companies.ToList());
        }

        public ActionResult RefreshMails()
        {
            using (
                var imap = new ImapClient("imap.gmail.com", "tobias.waggoner@gmail.com", "pass632274",
                    ImapClient.AuthMethods.Login, 993, true))
            {
                imap.SelectMailbox("[Google Mail]/All Mail");

                var selection = imap.SearchMessages(SearchCondition.SentSince(DateTime.Today.AddDays(-120)));
                var db = NetzalistDb.Instance;

                foreach (
                    var nxtMsg in
                        selection.Select(msg => msg.Value)
                            .Where(nxtMsg => !db.MailMessages.Any(i => i.MessageID == nxtMsg.MessageID)))
                {
                    AddMessageToDatabase(db, nxtMsg);
                }

                imap.Disconnect();
            }
            return RedirectToAction("Index");
        }

        private static void AddMessageToDatabase(NetzalistDb db, MailMessage nxtMsg)
        {
            var newMessagePK = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);

            var newMsg = new Models.DataModels.EMail.MailMessage
            {
                MailMessagePK = newMessagePK,
                From = GetMailAddress(db, nxtMsg.From).MailAddressPK,
                Sender = nxtMsg.Sender != null ? GetMailAddress(db, nxtMsg.Sender).MailAddressPK : (Guid?) null,
                Subject = nxtMsg.Subject,
                Body = nxtMsg.Body,
                ContentType = nxtMsg.ContentType,
                DateTimeSent = nxtMsg.Date,
                Importance = TranslateImportance(nxtMsg.Importance),
                Flags = TranslateFlags(nxtMsg.Flags),
                Encoding = nxtMsg.Encoding.CodePage,
                MessageID = nxtMsg.MessageID,
                Size = nxtMsg.Size,
                Uid = nxtMsg.Uid
            };

            db.MailMessages.Add(newMsg);

            var newRecipient = new MailRecipient
            {
                MailRecipientPK =
                    SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                RecipientType = MailRecipientType.From,
                MailMessagePK = newMessagePK,
                RecipientPK = newMsg.From
            };
            db.MailRecipients.Add(newRecipient);

            if (newMsg.Sender != null)
            {
                newRecipient = new MailRecipient
                {
                    MailRecipientPK =
                        SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                    RecipientType = MailRecipientType.Sender,
                    MailMessagePK = newMessagePK,
                    RecipientPK = newMsg.From
                };
                db.MailRecipients.Add(newRecipient);
            }

            foreach (var nxtAddress in nxtMsg.To)
                AddRecipient(db, newMsg.MailMessagePK, nxtAddress, MailRecipientType.To);

            foreach (var nxtAddress in nxtMsg.Cc)
                AddRecipient(db, newMsg.MailMessagePK, nxtAddress, MailRecipientType.CC);

            foreach (var nxtAddress in nxtMsg.Bcc)
                AddRecipient(db, newMsg.MailMessagePK, nxtAddress, MailRecipientType.BCC);

            db.SaveChanges();
        }

        private static MailFlags TranslateFlags(Flags flags)
        {
            var result = new MailFlags();
            if (flags.HasFlag(Flags.Answered))
                result = result | MailFlags.Answered;
            if (flags.HasFlag(Flags.Deleted))
                result = result | MailFlags.Deleted;
            if (flags.HasFlag(Flags.Draft))
                result = result | MailFlags.Draft;
            if (flags.HasFlag(Flags.Flagged))
                result = result | MailFlags.Flagged;
            if (flags.HasFlag(Flags.None))
                result = result | MailFlags.None;
            if (flags.HasFlag(Flags.Seen))
                result = result | MailFlags.Seen;
            return result;
        }

        private static MailPriority TranslateImportance(AE.Net.Mail.MailPriority priority)
        {
            if (priority == AE.Net.Mail.MailPriority.High)
                return MailPriority.High;
            if (priority == AE.Net.Mail.MailPriority.Low)
                return MailPriority.Low;

            return MailPriority.Normal;
        }

        private static void AddRecipient(NetzalistDb db, Guid msgPK, MailAddress nxtAddress,
            MailRecipientType recipientType)
        {
            var newRecipient = new MailRecipient
            {
                MailRecipientPK =
                    SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                RecipientType = recipientType,
                MailMessagePK = msgPK,
                RecipientPK = GetMailAddress(db, nxtAddress).MailAddressPK
            };
            db.MailRecipients.Add(newRecipient);
        }

        private static Models.DataModels.EMail.MailAddress GetMailAddress(NetzalistDb db, MailAddress address)
        {
            var lowerAddress = address.Address.ToLowerInvariant();
            var savedAddress = db.MailAddresses.FirstOrDefault(i => i.Address == lowerAddress);
            if (savedAddress != null) return savedAddress;

            savedAddress = new Models.DataModels.EMail.MailAddress
            {
                MailAddressPK = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                Address = lowerAddress,
                DisplayName = address.DisplayName,
                Host = address.Host,
                User = address.User
            };
            db.MailAddresses.Add(savedAddress);
            return savedAddress;
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        public ActionResult Create(Company company)
        {
            ModelState.Clear();
            var session = (Session) Session["Session"];
            company.CreatedByUserId = session.LogOnUser.LogOnUserId;
            company.TenantId = session.LogOnUser.Tenant.TenantId;
            company.CreationDate = DateTime.Now;

            TryValidateModel(company);
            if (!ModelState.IsValid) return View(company);
            var db = NetzalistDb.Instance;
            db.Companies.Add(company);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            ViewBag.Emails = new List<EmailViewModel>();

            var mailAddress = company.Email == null ? null : company.Email.ToLower();
            if (mailAddress == null) return View(company);
            
            var address = NetzalistDb.Instance.MailAddresses.FirstOrDefault(i => i.Address == mailAddress);
            if (address == null) return View(company);

            var list = GetAllMailsForMailAddress(address);
            ViewBag.Emails = list;

            return View(company);
        }

        private List<EmailViewModel> GetAllMailsForMailAddress(Models.DataModels.EMail.MailAddress address)
        {
            var mails = (
                from nxtMsg in NetzalistDb.Instance.MailMessages
                join nxtRecipient in NetzalistDb.Instance.MailRecipients
                    on nxtMsg.MailMessagePK equals nxtRecipient.MailMessagePK
                where
                    nxtRecipient.RecipientPK == address.MailAddressPK
                orderby nxtMsg.DateTimeSent descending
                select nxtMsg).ToList();

            var list = mails.Select(CreateEmailViewModelFromMailMessage).ToList();
            return list;
        }

        private EmailViewModel CreateEmailViewModelFromMailMessage(Models.DataModels.EMail.MailMessage nxtMail)
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

        //
        // POST: /Company/Edit/5

        [HttpPost]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                var db = NetzalistDb.Instance;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var db = NetzalistDb.Instance;
            var company = db.Companies.Find(id);
            db.Companies.Remove(company);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}