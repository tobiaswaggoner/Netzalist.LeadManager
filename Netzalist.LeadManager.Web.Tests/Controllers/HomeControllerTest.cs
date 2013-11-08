// *********************************************************************
// (c) 2013 Rope Development
// *********************************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using AE.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Netzalist.LeadManager.Web.Controllers;

namespace Netzalist.LeadManager.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            using (var imap = new AE.Net.Mail.ImapClient("imap.gmail.com", "tobias.waggoner@gmail.com", "pass632274", AE.Net.Mail.ImapClient.AuthMethods.Login, 993, true))
            {
                var listMailboxes = imap.ListMailboxes(string.Empty, "*");

                foreach (var listMailbox in listMailboxes)
                    Debug.WriteLine(listMailbox.Name);

                imap.SelectMailbox("[Google Mail]/All Mail");
                var messages = imap.GetMessageCount();


                var selection = imap.SearchMessages(SearchCondition.SentSince(DateTime.Today));
                foreach (var msg in selection)
                {
                    Debug.WriteLine(msg.Value.Uid + "<From: " + msg.Value.From + "> <To:" +msg.Value.To.ToList()[0].Address + "> " +  msg.Value.Subject);
                    
                    if(msg.Value.Attachments.Count>0)
                        foreach (var attachment in msg.Value.Attachments)
                            Debug.WriteLine("   " + attachment.ContentType + ": " + attachment.Filename);
                }

                imap.Disconnect();
            }
        }


    }
}