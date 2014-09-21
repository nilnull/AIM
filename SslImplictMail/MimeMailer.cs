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
using System.Net.Mail;

namespace SslImplicitMail
{
    /// <summary>
    /// Generate Mime Messages
    /// </summary>
    class MimeMailer : IMailer
    {
        /// <summary>
        /// Server Password
        /// </summary>
        private string _passWord;
        /// <summary>
        /// Show if server is using ssl or not
        /// </summary>
        private bool _isSsl;
        /// <summary>
        /// Port number of server
        /// </summary>
        private int _port;

        /// <summary>
        /// User name of user's mail
        /// </summary>
        private string _userName;

        /// <summary>
        /// Url address of mail server
        /// </summary>
        private string _host;

        /// <summary>
        /// Indecate if we need to send mail as html or plain text
        /// </summary>
        private readonly bool _useHtml;

        /// <summary>
        /// Display name of sender
        /// </summary>
        private readonly string _senderDisplayName;
        /// <summary>
        /// Email Address of sender
        /// </summary>
        private readonly string _senderEmailAddresss;
        /// <summary>
        /// Indicate if ssl server is implicit server or explicit
        /// </summary>
        private readonly bool _implictSsl;

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
        public MimeMailer(string host, int port, string userName, string passWord, bool isSsl, string senderDisplayName, string senderEmailAddresss, bool useHtml, bool implictSsl, MailPriority messagePriority)
        {
            _host = host;
            _port = port;
            _userName = userName;
            _passWord = passWord;
            _isSsl = isSsl;
            _senderDisplayName = senderDisplayName;
            _senderEmailAddresss = senderEmailAddresss;
            _useHtml = useHtml;
            _implictSsl = implictSsl;
            _messagePriority = messagePriority;
        }

        /// <summary>
        /// Generate ann email message
        /// </summary>
        /// <param name="toAddresses">Recievers</param>
        /// <param name="ccAddresses">CC list</param>
        /// <param name="bccAddresses">BCC List</param>
        /// <param name="attachmentsList">List of files attached to this mail</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="body">Message's busy</param>
        /// <returns>Generated message</returns>
        public AbstractMailMessage GenerateMail(List<IMailAddress> toAddresses, List<IMailAddress> ccAddresses, List<IMailAddress> bccAddresses, List<string> attachmentsList,
            string subject, string body)
        {
            if (_implictSsl)
            {
                var msg = new MimeMailMessage
                {
                    From = new MailAddress(_senderEmailAddresss, _senderDisplayName),
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
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="message">Email message that we want to send</param>
        /// <param name="onSendCallBack">The deligated function which will be called after sending message.</param>
        public void Send(AbstractMailMessage message, SendCompletedEventHandler onSendCallBack)
        {
            throw new NotImplementedException();
        }
    }
}
