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
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AegisImplicitMail
{
    /// <summary>
    /// Utilities used in SSL, SMTP and Smime Handeling
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// CMS encryption of a data
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="encryptionCertificates">A list of certificates to encrypt the data with</param>
        /// <returns>The encrypted data</returns>
        internal static byte[] Encrypt(string data, X509Certificate2Collection encryptionCertificates)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(data);
            EnvelopedCms envelopedCms = new EnvelopedCms(new ContentInfo(messageBytes));
            CmsRecipientCollection recipients = new CmsRecipientCollection(SubjectIdentifierType.IssuerAndSerialNumber, encryptionCertificates);
            envelopedCms.Encrypt(recipients); 
            return envelopedCms.Encode();
        }

        /// <summary>
        /// CMS (PKCS#7) signing of data
        /// </summary>
        /// <param name="data">Data to sign</param>
        /// <param name="signingCertificate">Certificate used for signing of message</param>
        /// <param name="encryptionCertificate">This certificate will be added to the end of signed text's certificate collection</param>
        /// <returns>signature</returns>
        internal static byte[] Sign(string data, X509Certificate2 signingCertificate, X509Certificate2 encryptionCertificate)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(data);

            SignedCms signedCms = new SignedCms(new ContentInfo(messageBytes), true);

            CmsSigner cmsSigner = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, signingCertificate);
            cmsSigner.IncludeOption = X509IncludeOption.WholeChain;

            if (encryptionCertificate != null)
            {
                cmsSigner.Certificates.Add(encryptionCertificate);
            }

            var signingTime = new Pkcs9SigningTime();
            cmsSigner.SignedAttributes.Add(signingTime);

            signedCms.ComputeSignature(cmsSigner, false);

            return signedCms.Encode();
        }


        /// <summary>
        /// Generate a random boundry for mail message
        /// </summary>
        /// <returns>A GUID based Boundry</returns>
        internal static string GenerateBoundary()
        {
            return "--CPI=_" + Guid.NewGuid().ToString();
        }


        /// <summary>
        /// Read an <see cref="System.Net.Mail.Attachment"/>Attachment and return the value of it in byte arrays.
        /// </summary>
        /// <param name="attachment">Attachment</param>
        /// <returns>byte array of attachment which is mainly used in encyption of it</returns>
        public static byte[] ReadAttachment(System.Net.Mail.Attachment attachment)
        {
            var reader = new BinaryReader(attachment.ContentStream);
            attachment.ContentStream.Position = 0;
            return reader.ReadBytes((int)attachment.ContentStream.Length);
        }
    }
    /// <summary>
    /// Login type for authentication.
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        /// No authentication is used.
        /// </summary>
        UseDefualtCridentials = -1,

        /// <summary>
        /// Base64 authentication is used.
        /// </summary>
        Base64 = 0,

        /// <summary>
        /// PlainText text authentication is used.
        /// </summary>
        PlainText = 1
    }



    /// <summary>
    /// List of SMTP reply codes.
    /// </summary>
    internal enum SmtpResponseCodes
    {
        SystemStatus = 211,
        Help = 214,
        Ready = 220,
        ClosingChannel = 221,
        RequestCompleted = 250,
        UserNotLocalOk = 251,
        StartInput = 354,
        ServiceNotAvailable = 421,
        MailBoxUnavailable = 450,
        RequestAborted = 451,
        InsufficientStorage = 452,
        Error = 500,
        SyntaxError = 501,
        CommandNotImplemented = 502,
        BadSequence = 503,
        CommandParameterNotImplemented = 504,
        MailBoxNotFound = 550,
        UserNotLocalBad = 551,
        ExceededStorage = 552,
        MailBoxNameNotValid = 553,
        TransactionFailed = 554
    }


}
