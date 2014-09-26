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
using System.Runtime.Remoting.Messaging;

namespace AegisImplicitMail
{
    /// <summary>
    /// Generate Mime Messages
    /// </summary>
    public class MimeMailer :SmtpSocketClient, IMailer 
    {

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

        public bool EnableImplicitSsl
        {
            get { return _implictSsl; }
            set { _implictSsl = value; }
        }

        /// <summary>
        /// Indecate if we need to send mail as html or plain text
        /// </summary>
        private readonly bool _useHtml;


        /// <summary>
        /// Indicate if ssl server is implicit server or explicit
        /// </summary>
        private  bool _implictSsl;

        private readonly AuthenticationType _authenticationType;

        /// <summary>
        /// Priority of email
        /// </summary>
        private MailPriority _messagePriority;

        /// <summary>
        /// Construct an emailer object to send mime mail
        /// </summary>
        /// <param name="host">Host address of server</param>
        /// <param name="port">Port number of server</param>
        /// <param name="userName">User name</param>
        /// <param name="passWord">Password</param>
        /// <param name="isSsl">Is it a Ssl/Tls server?</param>
        /// <param name="senderDisplayName">Sender's name</param>
        /// <param name="senderEmailAddresss">Sender's email address</param>
        /// <param name="useHtml">Are we going to send this email as a html message or a plain text?</param>
        /// <param name="implictSsl">Indicate if the ssl is an implict ssl</param>
        /// <param name="messagePriority">Priority of message</param>
        public MimeMailer(string host, int port = 465, string userName = null, string passWord ="", bool isSsl = false, bool implictSsl = false, MailPriority messagePriority = MailPriority.Normal, AuthenticationType authenticationType = AuthenticationType.PlainText):base(host,port)
        {
            Host = host;
            Port = port;
            User = userName;
            Password = passWord;
            EnableSsl = isSsl;
            _implictSsl = implictSsl;
            _messagePriority = messagePriority;
            _authenticationType = authenticationType;
        }

        public MimeMailer(string host):base(host)
        {
            
        }

        public MimeMailer(string host, int port):base(host,port)
        {
        }

        public MimeMailer()
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
                    Body = body,
                    IsBodyHtml = _useHtml
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
        public void Send(AbstractMailMessage message, SendCompletedEventHandler onSendCallBack)
        {

            if (_implictSsl)
            {
                SendCompleted += onSendCallBack;
                SendMailAsync(message);
                // Connecting to the server and configuring it
                using (var client = new SmtpSocketClient())
                {
                    Host = Host;
                    Port = Port;
                    EnableSsl = EnableSsl;
                    if (String.IsNullOrEmpty(User))
                        AuthenticationMode = AuthenticationType.UseDefualtCridentials;
                }
            }
            else
            {
                using (var client = new SmtpClient())
                {
                 
                    client.Host = Host;
                    client.Port = Port;
                    client.EnableSsl = EnableSsl;
                    if (String.IsNullOrEmpty(User))
                        client.UseDefaultCredentials =true;
                    else
                    {
                        client.Credentials = new NetworkCredential(User,Password);
                    }
                    client.SendCompleted += onSendCallBack;
                    client.SendMailAsync(message);
                  
                }
                
            }

        }
    }
}
