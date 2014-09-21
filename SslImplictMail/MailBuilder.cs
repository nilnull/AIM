/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Ssl Mailer (AISM)
 * Aegis Implicit Ssl Mailer is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
 * Aegis Implicit Ssl Mailer is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SslImplicitMail
{
    /// <summary>
    /// Single class to handle all types of mails in one place
    /// </summary>
    class MailBuilder
    {
        private readonly MailPriority _messagePriority;

        public MailBuilder(string host, int port, string userName, string passWord, string senderEmailAddresss, string senderDisplayName, bool isSsl = false, bool useHtml = true, bool implictSsl = false, bool smime = false, MailPriority messagePriority = MailPriority.Normal, SendCompletedEventHandler onSendCallBack = null, bool encrypt =false, bool sign = false)
        {
            _passWord = passWord;
            _isSsl = isSsl;
            _port = port;
            _userName = userName;
            _host = host;
            _useHtml = useHtml;
            _senderDisplayName = senderDisplayName;
            _senderEmailAddresss = senderEmailAddresss;
            _implictSsl = implictSsl;
            _smime = smime;
            _onSendCallBack = onSendCallBack;
            this._encrypt = encrypt;
            this._sign = sign;
            this._messagePriority = messagePriority;
        }

        public void SendMail()
        {
            
        }



        public  bool MailSent = false;
        private static AsyncCompletedEventArgs SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;
            return e;
        }

        public void SendSecureMail(List<SmimeMailAddress> recieverMailAddresses, string messageSubject, string messageBody, X509Certificate2 signingCertificate2, List<string> attachmentAddress = null, X509Certificate2 encryptionCertificate2 = null)
        {
            if (signingCertificate2 == null)
                throw new ArgumentNullException("signingCertificate2");

            SmimeMailMessage message = new SmimeMailMessage();

            // Look up your signing cert by serial number in your cert store

            if (encryptionCertificate2 == null && _encrypt)
                message.From = new SmimeMailAddress(_senderEmailAddresss,_senderDisplayName, signingCertificate2, signingCertificate2);
            else if (_encrypt && _sign)
            {
                message.From = new SmimeMailAddress(_senderEmailAddresss,_senderDisplayName, signingCertificate2, encryptionCertificate2);
            }
            else
            {
                message.From = new SmimeMailAddress(_senderEmailAddresss,_senderDisplayName, signingCertificate2);
            }
            recieverMailAddresses.ForEach(a =>
                message.To.Add(a));
            if (attachmentAddress != null && attachmentAddress.Count > 0)
                attachmentAddress.ForEach(a => message.Attachments.Add(new MimeAttachment(a)));
            message.Priority = _messagePriority;
            message.Subject = messageSubject;

            message.Body = messageBody;
            message.IsBodyHtml = _useHtml;

            message.IsSigned = _sign;

            message.IsEncrypted = _encrypt;


            SendMessage(message,_onSendCallBack);
        }

        private readonly bool _useHtml;

        private void SmtpSendMessage(AbstractMailMessage msg, SendCompletedEventHandler OnMailSent)
        {


            // Connecting to the server and configuring it
            using (var client = new SmtpSocketClient())
            {
                client.Host = _host;
                client.Port = _port;
                client.EnableSsl = _isSsl;
                if (String.IsNullOrEmpty(_userName))
                    client.AuthenticationMode = AuthenticationType.UseDefualtCridentials;
                else
                {
                    client.AuthenticationMode = _base64Authentication ? AuthenticationType.Base64 : AuthenticationType.PlainText;
                    client.Password = _passWord;
                    client.User = _userName;
                }

                client.OnMailSent += new SendCompletedEventHandler(_onSendCallBack);
                client.SendMessageAsync(msg);
            }
        }

        public void SendImplicitMail(List<MailAddress> recieverMailAddresses, MailPriority messagePriority, string messageSubject, string messageBody, List<string> attachmentAddress = null, List<MailAddress> ccMailAddresses = null, List<MailAddress> bccMailAddresses = null)
        {
            var msg = new MimeMailMessage();
            msg.From = new MailAddress(_senderEmailAddresss,_senderDisplayName);
            msg.Subject = messageSubject;
            msg.Body = messageBody;
            msg.IsBodyHtml = _useHtml;
            recieverMailAddresses.ForEach(a => msg.To.Add(a));
            if (ccMailAddresses != null) ccMailAddresses.ForEach(a => msg.To.Add(a));
            if (bccMailAddresses != null) bccMailAddresses.ForEach(a => msg.To.Add(a));

            if (attachmentAddress != null)
                attachmentAddress.ForEach(a=> msg.Attachments.Add(new MimeAttachment(a)));
    
            SmtpSendMessage(msg,null);
        }

        public void SendMail(List<MailAddress> recieverMailAddresses, MailPriority messagePriority, string messageSubject, string messageBody, List<string> attachmentAddress = null, SendCompletedEventHandler onSendCallBack = null, List<MailAddress> ccMailAddresses = null, List<MailAddress> bccMailAddresses = null)
        {
            // C# Code
            MailSent = false;
            MailMessage msg = new MailMessage();

            // Your mail address and display name.
            // This what will appear on the From field.
            // If you used another credentials to access
            // the SMTP server, the mail message would be
            // sent from the mail specified in the From
            // field on behalf of the real sender.
            msg.From = new MailAddress(_senderEmailAddresss, _senderDisplayName);

            // To addresses

            recieverMailAddresses.ForEach(a => msg.To.Add(a));
            if (ccMailAddresses != null) ccMailAddresses.ForEach(a => msg.To.Add(a));
            if (bccMailAddresses != null) bccMailAddresses.ForEach(a => msg.To.Add(a));

            // You can specify CC and BCC addresses also

            // Set to high priority
            msg.Priority = messagePriority;

            msg.Subject = messageSubject;

            // You can specify a plain text or HTML contents
            msg.Body = messageBody;
            // In order for the mail client to interpret message
            // body correctly, we mark the body as HTML
            // because we set the body to HTML contents.
            if (_useHtml)
                msg.IsBodyHtml = true;

            // Attaching some data

            if (attachmentAddress != null && attachmentAddress.Count > 0)
                attachmentAddress.ForEach(a => msg.Attachments.Add(new Attachment(a)));


            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Priority = MailPriority.High;

            SendMessage(msg,_onSendCallBack);
        }

        private readonly string _senderDisplayName;

        private readonly string _senderEmailAddresss;

        private void SendMessage(MailMessage msg, SendCompletedEventHandler onSendCallBack)
        {


            // Connecting to the server and configuring it
            using (var client = new SmtpClient())
            {
                client.Host = _host;
                client.Port = _port;
                client.EnableSsl = _isSsl;
                if (String.IsNullOrEmpty(_userName))
                    client.UseDefaultCredentials = true;
                else
                {
                    client.UseDefaultCredentials = false;

                    string pass = _passWord;
                    // Provide your credentials
                    client.Credentials = new System.Net.NetworkCredential(_userName,
                        pass);
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                // Use SendAsync to send the message asynchronously
                //    client.Send(msg);
                client.SendCompleted += new
                    SendCompletedEventHandler(onSendCallBack);
                // The userState can be any object that allows your callback  
                // method to identify this send operation. 
                // For this example, the userToken is a string constant. 
                const string userState = "test message1";
                client.Send(msg);
            }
        }

        private readonly string _passWord;

        private readonly bool _isSsl;

        private readonly int _port;

        private readonly string _userName;

        private readonly string _host;
        private readonly bool _implictSsl;
        private readonly bool _smime;
        private SendCompletedEventHandler _onSendCallBack = null;
        private bool _base64Authentication;
        private readonly bool _encrypt;
        private readonly bool _sign;
    }
}
