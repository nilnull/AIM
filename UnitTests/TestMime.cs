using System;
using System.ComponentModel;
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
            var mymessage = new MimeMailMessage {Subject = "test", Body = "body"};
            mymessage.To.Add(mail);
            mymessage.From = new MimeMailAddress(mail);
       

            var mailer = new MimeMailer(host, 465, user, pass, SslMode.Ssl,
                AuthenticationType.Base64);
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);
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
