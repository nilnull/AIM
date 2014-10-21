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
using System.Security.Cryptography.X509Certificates;

namespace AegisImplicitMail
{
    /// <summary>
    /// SMIME Email sender 
    /// </summary>
    public class SmimeMailer : MimeMailer
    {
        private readonly bool _encrypt;
        private readonly bool _sign;
       
        public SmimeMailAddress From { get; set; }
        private List<IMailAddress> to;
        private List<SmimeMailAddress> cc;
        private List<SmimeMailAddress> bcc;


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
        /// <param name="sslType">Defines type of Ssl that your server uses, if you want to send Implicit Ssl mail use ssl, if explicit it should be TLS</param>
        /// <param name="implictSsl">Use implicit ssl</param>
        /// <param name="sign">Do you need to sign mail?</param>
        /// <param name="toSigningCerts"></param>
        /// <param name="toEncryptionCerts"></param>
        /// <param name="authenticationType"></param>
        /// <param name="encrypt"></param>
        public SmimeMailer(string host, int port, string userName, string passWord, string senderEmailAddresss,
            string senderDisplayName, X509Certificate2 signingCertificate2 = null,
            X509Certificate2 encryptionCertificate2 = null, SslMode sslType = SslMode.None, 
            bool sign = false,
            List<X509Certificate2> toSigningCerts = null, List<X509Certificate2> toEncryptionCerts = null,
            AuthenticationType authenticationType = AuthenticationType.Base64, bool encrypt = true):base(host,port,userName,passWord,sslType,authenticationType)
        {
            if (encryptionCertificate2 == null || toEncryptionCerts == null)
                throw new ArgumentNullException("encryptionCertificate2");
            if (sign && (signingCertificate2 == null || toSigningCerts == null))
                throw new ArgumentNullException("signingCertificate2");
            _encrypt = encrypt;
            _sign = sign;
            From = new SmimeMailAddress(senderEmailAddresss,senderDisplayName,encryptionCertificate2,signingCertificate2);
            
        }

        public SmimeMailer(string host, int port):base(host,port) { }


        public SmimeMailer(string host) : base(host) { }
        public SmimeMailer() : base() { }


        public SmimeMailMessage GenerateMail(List<IMailAddress> toAddresses,
            List<SmimeMailAddress> ccAddresses, List<SmimeMailAddress> bccAddresses,
            List<string> attachmentsList, string subject, string body)
        {
            if (toAddresses == null)
            {
                throw new ArgumentNullException("toAddresses");
            }


            var message = new SmimeMailMessage();

            to = toAddresses;
            cc = ccAddresses;
            bcc = bccAddresses;



            message.From = From;
            to.ForEach(a =>
                message.To.Add((SmimeMailAddress) a));
            if (cc != null)
                cc.ForEach(a =>
                    message.CC.Add(a));
            if (bcc != null)
                bcc.ForEach(a =>
                    message.Bcc.Add(a));

            if (attachmentsList != null && attachmentsList.Count > 0)
                attachmentsList.ForEach(a => message.Attachments.Add(new MimeAttachment(a)));
            
            message.Subject = subject;

            message.Body = body;
            
            message.IsSigned = _sign;

            message.IsEncrypted = _encrypt;

            return message;

        }
    }
}
