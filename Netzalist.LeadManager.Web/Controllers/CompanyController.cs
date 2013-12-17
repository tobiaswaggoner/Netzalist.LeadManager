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
using Netzalist.LeadManager.Web.Models.ViewModels.Leads;
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
            return View(ServiceFactory.GetLeadService().GetAllCompanies());
        }

        public ActionResult RefreshMails()
        {

            using (
                var imap = new ImapClient("imap.gmail.com", "tobias.waggoner@gmail.com", "pass632274",
                    ImapClient.AuthMethods.Login, 993, true))
            {
                imap.SelectMailbox("[Google Mail]/All Mail");

                var selection = imap.SearchMessages(SearchCondition.SentSince(DateTime.Today.AddDays(-120)));

                foreach (
                    var nxtMsg in
                        selection.Select(msg => msg.Value)
                            .Where(nxtMsg => !ServiceFactory.GetEmailService().MessageExists(nxtMsg.MessageID)))
                {
                    AddMessageToDatabase( nxtMsg);
                }

                imap.Disconnect();
            }
            return RedirectToAction("Index");
        }

        private static void AddMessageToDatabase(MailMessage nxtMsg)
        {
            var newMessagePK = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);

            var addresses = new List<Models.DataModels.EMail.MailAddress>();
            var recipients = new List<MailRecipient>();

            var from = GetMailAddress(addresses, nxtMsg.From);
            var sender = nxtMsg.Sender != null ? GetMailAddress(addresses, nxtMsg.Sender) : null;

            var newMsg = new Models.DataModels.EMail.MailMessage
            {
                MailMessagePK = newMessagePK,
                From = from.MailAddressPK,
                Sender = sender != null ? sender.MailAddressPK : (Guid?)null,
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

            var newRecipient = new MailRecipient
            {
                MailRecipientPK =
                    SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                RecipientType = MailRecipientType.From,
                MailMessagePK = newMessagePK,
                RecipientPK = newMsg.From
            };
            addresses.Add(from);
            recipients.Add(newRecipient);


            if (newMsg.Sender != null)
            {
                newRecipient = new MailRecipient
                {
                    MailRecipientPK =
                        SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                    RecipientType = MailRecipientType.Sender,
                    MailMessagePK = newMessagePK,
                    RecipientPK = newMsg.Sender.Value
                };
                addresses.Add(sender); 
                recipients.Add(newRecipient);
            }

            foreach (var nxtAddress in nxtMsg.To)
                AddRecipient(addresses, recipients, newMsg.MailMessagePK, nxtAddress, MailRecipientType.To);

            foreach (var nxtAddress in nxtMsg.Cc)
                AddRecipient(addresses, recipients, newMsg.MailMessagePK, nxtAddress, MailRecipientType.CC);

            foreach (var nxtAddress in nxtMsg.Bcc)
                AddRecipient(addresses, recipients, newMsg.MailMessagePK, nxtAddress, MailRecipientType.BCC);

            ServiceFactory.GetEmailService().AddMailMessage(newMsg, recipients, addresses);
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

        private static void AddRecipient(List<Models.DataModels.EMail.MailAddress> addresses, List<MailRecipient> recipients, Guid msgPK, MailAddress nxtAddress,
            MailRecipientType recipientType)
        {
            var newRecipient = new MailRecipient
            {
                MailRecipientPK =
                    SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                RecipientType = recipientType,
                MailMessagePK = msgPK,
                RecipientPK = GetMailAddress(addresses, nxtAddress).MailAddressPK
            };
            recipients.Add(newRecipient);
        }

        private static Models.DataModels.EMail.MailAddress GetMailAddress(List<Models.DataModels.EMail.MailAddress> addresses, MailAddress address)
        {
            var savedAddress = ServiceFactory.GetEmailService().FindAddressByAddress(address.Address);
            if (savedAddress != null) return savedAddress;

            savedAddress = new Models.DataModels.EMail.MailAddress
            {
                MailAddressPK = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAtEnd),
                Address = address.Address.ToLowerInvariant(),
                DisplayName = address.DisplayName,
                Host = address.Host,
                User = address.User
            };
            addresses.Add(savedAddress);
            return savedAddress;
        }

        //
        // GET: /Company/Details/5

        public ActionResult Details(int id = 0)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);
            var persons = from nxtPerson in NetzalistDb.Instance.Persons select new PersonViewModel(nxtPerson, null);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(new CompanyViewModel(company, persons.ToList()));
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
        public ActionResult Create(CompanyViewModel companyViewModel)
        {
            var session = (Session) Session["Session"];
            var company = new Company
            {
                CreatedByUserId = session.LogOnUser.LogOnUserId,
                TenantId = session.LogOnUser.Tenant.TenantId,
                CreationDate = DateTime.Now
            };
            companyViewModel.SyncDataModel(company);

            ModelState.Clear();
            TryValidateModel(company);
            if (!ModelState.IsValid) return View(companyViewModel);

            var db = NetzalistDb.Instance;
            db.Companies.Add(company);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id = 0, String filter=null)
        {
            var company = NetzalistDb.Instance.Companies.Find(id);
            var persons = (from nxtPerson in NetzalistDb.Instance.Persons select nxtPerson).ToList();

            if (company == null)
            {
                return HttpNotFound();
            }
            var companyViewModel = new CompanyViewModel(company, persons.Select(i=>new PersonViewModel(i, null)).ToList())
            {
                ContactToEdit = _contactToEdit
            };
            //LoadMails(filter, company, companyViewModel);

            return View(companyViewModel);
        }

        private void LoadMails(string filter, Company company, CompanyViewModel companyViewModel)
        {
            ViewBag.Emails = new List<EmailViewModel>();

            var mailAddress = company.Email == null ? null : company.Email.ToLower();
            if (mailAddress == null)
                return;

            var address = NetzalistDb.Instance.MailAddresses.FirstOrDefault(i => i.Address == mailAddress);
            if (address == null)
                return;

            ViewBag.Emails = GetAllMailsForMailAddress(address, filter);
        }


        private List<EmailViewModel> GetAllMailsForMailAddress(Models.DataModels.EMail.MailAddress address, String filter)
        {
            var mails = (
                from nxtMsg in NetzalistDb.Instance.MailMessages
                join nxtRecipient in NetzalistDb.Instance.MailRecipients
                    on nxtMsg.MailMessagePK equals nxtRecipient.MailMessagePK
                where
                    nxtRecipient.RecipientPK == address.MailAddressPK
                orderby nxtMsg.DateTimeSent descending
                select nxtMsg).ToList();

            var filterList = from nxtMsg in mails group nxtMsg by nxtMsg.DateTimeSent.ToString("MMMM yyyy");

            ViewBag.FilterList = (from x in filterList select x.Key + " (" + x.Count() + ")").ToList();

            if (filter == null) return mails.Take(200).Select(CreateEmailViewModelFromMailMessage).ToList();
            
            var period = filter.Substring(0, filter.IndexOf('(') - 1);
            return mails.Where(i => i.DateTimeSent.ToString("MMMM yyyy") == period ).Take(200).Select(CreateEmailViewModelFromMailMessage).ToList();
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
        public ActionResult Edit(CompanyViewModel companyViewModel)
        {
            var company = NetzalistDb.Instance.Companies.Find(companyViewModel.CompanyId);
            companyViewModel.SyncDataModel(company);
            companyViewModel.ContactToEdit = _contactToEdit;

            if (ModelState.IsValid)
            {
                var db = NetzalistDb.Instance;
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
            }
            //LoadMails(null, company, companyViewModel);

            return View(companyViewModel);
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
            return View(new CompanyViewModel(company, null));
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

        private PersonViewModel _contactToEdit;

        public ActionResult AddContact(int companyId)
        {
            _contactToEdit = new PersonViewModel();
            return RedirectToAction("Edit", new{ id= companyId, filter=(String)null});
        }

        public ActionResult SaveContact(int companyId)
        {
            _contactToEdit = null;
            return RedirectToAction("Edit", new { id = companyId, filter = (String)null });
        }
    }
}