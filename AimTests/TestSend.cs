using System.ComponentModel;
using AegisImplicitMail;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
         string mail = "you@gmail.com";
        string host = "smtp.gmail.com";
        string user = "yourUserName";
        string pass = "yourPassword";        
        MimeMailMessage mymessage;
        MimeMailer mailer;
         private void SendEmail()
    {
       
        
            //Create message
            mymessage = new MimeMailMessage();
            mymessage.From = new MimeMailAddress(mail);
            mymessage.To.Add(mail);
            mymessage.Subject = "test";
                mymessage.Body = "body";        
            //Set a delegate function for call back
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);
    }

    //Call back function
    private void compEvent(object sender, AsyncCompletedEventArgs e)
    {       
       Assert.IsEmpty(e.Error.Message);
    }
        [SetUp]
        public void Setup()
        {
                  //Create Smtp Client
                mailer = new MimeMailer(host, 465);
                mailer.User= user;
                mailer.Password = pass;
                mailer.SslType = SslMode.Ssl;
                mailer.AuthenticationMode = AuthenticationType.Base64;
        }

        [Test]
        public void TestSender()
        {
          
            SendEmail();
            Assert.Pass();
        }
    }
}