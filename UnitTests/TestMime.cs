using System;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading;
using AegisImplicitMail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestMime
    {
        [TestMethod]
        public void TestImplictSsl()
        {
            const string mail = "you@gmail.com";
            const string host = "smtp.gmail.com";
            const string user = "you@gmail.com";
            const string pass = "yourPassword";
            var mymessage = new MimeMailMessage {Subject = "test with attachment", Body = "body"};
            mymessage.To.Add(mail);
            mymessage.From = new MimeMailAddress(mail);

            var attachmentContent = "Hello! I'm the textual content of this attachment";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(attachmentContent));
            var attachment = new MimeAttachment(ms, "Important message.txt", new ContentType("text/plain"));
            mymessage.Attachments.Add(attachment);
            
            var mailer = new MimeMailer(host, 465, user, pass, SslMode.Ssl,
                AuthenticationType.Base64);
            mailer.SendCompleted += compEvent;
            mailer.SendMail(mymessage);
        }

        private void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.UserState.ToString()))
                Console.Out.WriteLine(e.UserState.ToString());
            Console.Out.WriteLine("is it canceled? " + e.Cancelled);

            if (e.Error != null)
                Console.Out.WriteLine("Error : " + e.Error.Message);

        }
    }
}
