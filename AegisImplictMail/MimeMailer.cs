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
using System.Net;
using System.Net.Mail;

namespace AegisImplicitMail
{
    /// <summary>
    /// Type of Ssl
    /// </summary>
    public enum SslMode
    {
        /// <summary>
        /// None Ssl Servers
        /// </summary>
        None = -1, 

        /// <summary>
        /// Explicit Ssl Servers
        /// </summary>
        Ssl = 0 ,
        /// <summary>
        /// Implicit Ssl Servers
        /// </summary>
        Tls = 1,

        /// <summary>
        /// todo:Automatically detect type of ssl
        /// </summary>
        Auto =2
    }

    /// <summary>
    /// Generate Mime Messages
    /// </summary>
    public class MimeMailer :SmtpSocketClient, IMailer 
    {

        /// <summary>
        /// Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.
        /// </summary>
        public NetworkCredential Credentials
        {
            get
            {
                return new NetworkCredential(User,Password);
            }
            set
            {
                User = value.UserName;
                Password = value.Password;
            }
        }

        private SslMode _ssltype = SslMode.None;
        public new SslMode SslType
        {
            get { return _ssltype;}
            set
            {
                switch (value)
                {
                    case SslMode.None:
                        break;
                    case SslMode.Ssl:
                        EnableImplicitSsl = true;
                        break;
                    case SslMode.Tls:
                        EnableImplicitSsl = false;
                        break;
                    case SslMode.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
                _ssltype = value;
                base.SslType = value;
            }
        }
        public bool EnableImplicitSsl
        {
            get { return _ssltype == SslMode.Ssl; }
            set
            {
             
                _ssltype = value ? SslMode.Ssl : SslMode.Tls;
            }
        }

    


       
        /// <summary>
        /// Construct an emailer object to send mime mail
        /// </summary>
        /// <param name="host">Host address of server</param>
        /// <param name="port">Port number of server</param>
        /// <param name="userName">User name</param>
        /// <param name="passWord">Password</param>
        /// <param name="sslType">Defines the type of encryption that your mail server uses</param>
        /// <param name="implictSsl">Indicate if the ssl is an implict ssl</param>
        /// <param name="authenticationType">Defines type of authentication that your smtp server uses</param>
        public MimeMailer(string host, int port = 465, string userName = null, string passWord ="", SslMode sslType = SslMode.None, AuthenticationType authenticationType = AuthenticationType.PlainText):base(host,port)
        {
            Host = host;
            Port = port;
            User = userName;
            Password = passWord;
            base.SslType = sslType;
            if (sslType == SslMode.Auto)
                SslType = DetectSslMode();
            AuthenticationMode = authenticationType;
        }

        /// <summary>
        /// Construct an emailer object to send mime mail
        /// </summary>
        /// <param name="host">Host address of mailing server</param>
        public MimeMailer(string host):base(host)
        {
            
        }
        /// <summary>
        /// Construct an emailer object to send mime mail
        /// </summary>
        /// <param name="host">Host address of mailing server</param>
        /// <param name="port">Port number of server</param>
        public MimeMailer(string host, int port):base(host,port)
        {
        }

        /// <summary>
        /// Construct an emailer object to send mime mail
        /// </summary>
        public MimeMailer():base()
        {
        }


        /// <summary>
        /// Generate ann email message
        /// </summary>
        /// <param name="sender">From field of mail</param>
        /// <param name="toAddresses">Recievers</param>
        /// <param name="ccAddresses">CC list</param>
        /// <param name="bccAddresses">BCC List</param>
        /// <param name="attachmentsList">List of files attached to this mail</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="body">Message's busy</param>
        /// <returns>Generated message</returns>
        public AbstractMailMessage GenerateMail(IMailAddress sender, List<IMailAddress> toAddresses, List<IMailAddress> ccAddresses, List<IMailAddress> bccAddresses, List<string> attachmentsList,
            string subject, string body)
        {
                var msg = new MimeMailMessage
                {
                    From = (MailAddress)sender,
                    Subject = subject,
                    Body = body
                };
                toAddresses.ForEach(a => msg.To.Add((MimeMailAddress) a));
                if (ccAddresses != null) ccAddresses.ForEach(a => msg.To.Add((MimeMailAddress) a));
                if (bccAddresses != null) bccAddresses.ForEach(a => msg.To.Add((MimeMailAddress) (a)));

                if (attachmentsList != null)
                    attachmentsList.ForEach(a => msg.Attachments.Add(new MimeAttachment(a)));
                return msg;
        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="message">Email message that we want to send</param>
        /// <param name="onSendCallBack">The deligated function which will be called after sending message.</param>
        public void Send(AbstractMailMessage message, SendCompletedEventHandler onSendCallBack = null)
        {

                SendCompleted += onSendCallBack;
                SendMailAsync(message);
                // Connecting to the server and configuring it
                using (var client = new SmtpSocketClient())
                {
                    if (String.IsNullOrEmpty(User))
                        AuthenticationMode = AuthenticationType.UseDefaultCredentials;
                }
            

        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="recipient">receiver email address</param>
        /// <param name="subject">email subject</param>
        /// <param name="body">message content</param>
        public void Send(string from, string recipient, string subject, string body)
        {
           var allowUnicode = this.IsUnicodeSupported();
            var mailMessage = new MimeMailMessage
            {
                From = new MimeMailAddress(@from),
           Subject = subject,
           Body = body
            
            };
            mailMessage.To.Add(new MimeMailAddress(recipient));
            Send(mailMessage);

        }

        private object IsUnicodeSupported()
        {
            return true;
        }
    }
}
