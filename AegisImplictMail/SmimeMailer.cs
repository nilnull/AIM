/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Mail (AIM)
 * Aegis Implicit Mail is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
 * Aegis Implicit Mail is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace AegisImplicitMail
{
    /// <summary>
    /// SMIME Email sender 
    /// </summary>
    class SmimeMailer : IMailer
    {
        private readonly string _passWord;
        private readonly bool _isSsl;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _host;
        private readonly bool _useHtml;
        private readonly string _senderDisplayName;
        private readonly string _senderEmailAddresss;
        private readonly bool _implictSsl;
        private readonly SendCompletedEventHandler _onSendCallBack;
        private readonly bool _encrypt;
        private readonly MailPriority _messagePriority;
        private readonly bool _sign;
        public X509Certificate2 SigningCert { get; set; }
        public X509Certificate2 EncryprionCert { get; set; }
        public AuthenticationType AuthenticationMode { get; set; }

        /// <summary>
        /// Initiate and construct a smime mailer object
        /// </summary>
        /// <param name="host">URL address of mail server</param>
        /// <param name="port">Port address of mail server</param>
        /// <param name="userName">User name of user</param>
        /// <param name="passWord">User's password to login to server</param>
        /// <param name="senderEmailAddresss">Email address of sender</param>
        /// <param name="senderDisplayName">Name of sender</param>
        /// <param name="signingCertificate2">Certificate that is user for signing, Note: You can set this with the same cert you use for encryption</param>
        /// <param name="encryptionCertificate2">Certificate that is used for Encryption of mail</param>
        /// <param name="isSsl">is the mail server using ssl?</param>
        /// <param name="useHtml">Send email body as html</param>
        /// <param name="implictSsl">Use implicit ssl</param>
        /// <param name="messagePriority">Mail priority</param>
        /// <param name="onSendCallBack">Call when mail is sent</param>
        /// <param name="sign">Do you need to sign mail?</param>
        /// <param name="toSigningCerts"></param>
        /// <param name="toEncryptionCerts"></param>
        /// <param name="authenticationType"></param>
        /// <param name="encrypt"></param>
        public SmimeMailer(string host, int port, string userName, string passWord, string senderEmailAddresss, string senderDisplayName, X509Certificate2 signingCertificate2 = null, X509Certificate2 encryptionCertificate2 = null, bool isSsl = false, bool useHtml = true, bool implictSsl = false, MailPriority messagePriority = MailPriority.Normal, SendCompletedEventHandler onSendCallBack = null, bool sign = false, List<X509Certificate2> toSigningCerts = null,  List<X509Certificate2> toEncryptionCerts = null, AuthenticationType authenticationType = AuthenticationType.Base64, bool encrypt=true)
        {
            if (encryptionCertificate2 == null || toEncryptionCerts ==null)
                throw  new ArgumentNullException("encryptionCertificate2");
            if (sign && (signingCertificate2==null || toSigningCerts == null))
                throw  new ArgumentNullException("signingCertificate2");
            AuthenticationMode = authenticationType;
            _encrypt = encrypt;
            _passWord = passWord;
            _isSsl = isSsl;
            _port = port;
            _userName = userName;
            _host = host;
            _useHtml = useHtml;
            _senderDisplayName = senderDisplayName;
            _senderEmailAddresss = senderEmailAddresss;
            _implictSsl = implictSsl;
            _onSendCallBack = onSendCallBack;
            _sign = sign;
            SigningCert = signingCertificate2;
            EncryprionCert = encryptionCertificate2;
          
            _messagePriority = messagePriority;
        }

        private List<IMailAddress> to;
        private List<IMailAddress> cc;
        private List<IMailAddress> bcc;

        public AbstractMailMessage GenerateMail(List<IMailAddress> toAddresses, List<IMailAddress> ccAddresses, List<IMailAddress> bccAddresses, List<string> attachmentsList, string subject, string body)
        {
            if (toAddresses == null)
            {
                throw new ArgumentNullException("toAddresses");
            }
       

            SmimeMailMessage message = new SmimeMailMessage();

            to = toAddresses;
            cc = ccAddresses;
            bcc = bccAddresses;


          
             message.From = _sign ? new SmimeMailAddress(_senderEmailAddresss, _senderDisplayName, SigningCert, EncryprionCert) : new SmimeMailAddress(_senderEmailAddresss, _senderDisplayName, SigningCert);
            to.ForEach(a =>
                message.To.Add((SmimeMailAddress)a));
            if (cc != null)
                cc.ForEach(a =>
                    message.CC.Add((SmimeMailAddress)a));
            if (bcc != null)
                bcc.ForEach(a =>
                    message.Bcc.Add((SmimeMailAddress)a));

            if (attachmentsList != null && attachmentsList.Count > 0)
                attachmentsList.ForEach(a => message.Attachments.Add(new MimeAttachment(a)));
            message.Priority = _messagePriority;
            message.Subject = subject;

            message.Body = body;
            message.IsBodyHtml = _useHtml;

            message.IsSigned = _sign;

            message.IsEncrypted = _encrypt;

            return message;

        }

        public void Send(AbstractMailMessage message, SendCompletedEventHandler onSendCallBack)
        {
            if (!_implictSsl)
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
                    client.Send((SmimeMailMessage) message);
                }
            }
            else
            {
                // Connecting to the server and configuring it
                using (var client = new SmtpSocketClient())
                {
                    MimeMailMessage smtpMail = (SmimeMailMessage) message;
                    client.Host = _host;
                    client.Port = _port;
                    client.EnableSsl = _isSsl;
                    if (String.IsNullOrEmpty(_userName))
                        client.AuthenticationMode = AuthenticationType.UseDefualtCridentials;
                    else
                    {
                        client.AuthenticationMode = AuthenticationMode;
                        client.Password = _passWord;
                        client.User = _userName;
                    }

                    client.OnMailSent += new SendCompletedEventHandler(onSendCallBack);
                    client.SendMessageAsync(message);
                }

            }
        }
        }
    }

