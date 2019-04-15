# Aegis Implicit Mail (AIM)
Aegis Implicit Mail is a free and open source library which is designed to provide fast and developer friendly API to send emails using SMTP ports. 
Please check https://sourceforge.net/projects/netimplicitssl/ for archive

#Features
AIM Supports :
* None Ssl Mails
* Implicit Ssl Mails
* Explicit Ssl Mails
* Smtp Connection test
* Detect Ssl type of mail server
* Attachments for MIME and S/MIME message.
* In-line attachment and separate file attachment
* All content types and mimes
* SMIME , Signed and Encrypted mails
In an integrated way 

# Why AIM
Sadly, Microsoft.Net.Mail Ssl Mails does not support Implicit Ssl Mail and it is still used by many servers, including port 465 of Gmail here AIM comes to make a readable and fast alternative to send your smtp mails.

## Download
Binaries are available in the form of NuGet package:
https://www.nuget.org/packages/AIM

Getting the Code
----------------
To get a local copy of the current code, clone it using git:
'''
$ git clone https://github.com/nilnull/AIM.git
''' 

Write the code!
---------------


If you want to have a sample SMTP mailing, you can write your program exactly like the way you write using 
System.Net.Mail
   ```csharp

    private void SendEmail()
    {
        var mail = "you@gmail.com";
        var host = "smtp.gmail.com";
        var user = "yourUserName";
        var pass = "yourPassword";
        
        //Generate Message 
        var mymessage = new MimeMailMessage();
        mymessage.From = new MimeMailAddress(mail);
        mymessage.To.Add(mail);
        mymessage.Subject = "test";
        mymessage.Body = "body";
        
        //Create Smtp Client
        var mailer = new MimeMailer(host, 465);
        mailer.User= user;
        mailer.Password = pass;
        mailer.SslType = SslMode.Ssl;
        mailer.AuthenticationMode = AuthenticationType.Base64;
        
        //Set a delegate function for call back
        mailer.SendCompleted += compEvent;
        mailer.SendMailAsync(mymessage);
    }

    //Call back function
    private void compEvent(object sender, AsyncCompletedEventArgs e)
    {
        if (e.UserState!=null)
            Console.Out.WriteLine(e.UserState.ToString());
        
        Console.Out.WriteLine("is it canceled? " + e.Cancelled);

        if (e.Error != null)
                Console.Out.WriteLine("Error : " + e.Error.Message);
    }
```



As you might be familiar with _System.Net.Mail_, we have four important objects in sending mails: 
1. Mail Message
2. Addresses
3. Attachment 
4. sender

For each mail you need to generate mail message, set addresses and attachments and then send it using a smtp sender. 

AIM uses the same architecture. We have normal (mime) Mails and smime Mails that can be in a plain sender or Ssl Sender in addition Ssl Sender can be implicit and explicit.

In Conclusion you can [Send Mime Mails to Implicit Ssl Smtp Mail Servers](Send Mime Mails to Implicit Ssl Smtp Mail Servers),[Send SMIME Mails to Implicit Ssl Smtp Mail Servers](Send SMIME Mails to Implicit Ssl Smtp Mail Servers), [Send Mime Mails to Smtp Mail Servers](Send Mime Mails to Smtp Mail Servers), [Send SMIME mails to smtp mail servers](Send SMIME mails to smtp mail servers), [Send Mime Mails to Explicit Ssl Smtp Mail Servers](Send Mime Mails to Explicit Ssl Smtp Mail Servers) and [Send SMIME Mails to Explicit Ssl Smtp Mail Servers](Send SMIME Mails to Explicit Ssl Smtp Mail Servers) using same an exact same logic.
Another alternative is to use our mail factory class which is able to send all mentioned mails  


# Features 
* Support for both of Smime and Mime Messages
* Easy to use and well designed, easy to migrate from System.Net.Mail
* Support for Signed and/or encrypted mail
* Supports Ssl Type detection (Detects if Mail server is using Implicit (SSL) or Explicit (TLS) smtp)
* Support Base64, PlainText and default credential Athentication
* Asynchronous Mail Sending
* Support None Ssl Mails
* Support Inline and Attached Attachments.
* Signed and Encrypted attachments
* Fast and reliable
* Made by developers for developers
* Test Connection Settings
* Auto Detection of Ssl Type
* Support Office 365
* Support display name



# Development Team
AIM is delivered by NilNull from PKI.Tools with special thanks to those who help us :
* Eugene Jenihov
* Dénes Gál-Szász
* galdenny

