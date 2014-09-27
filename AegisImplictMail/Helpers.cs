#region

using System;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

#endregion

namespace AegisImplicitMail
{
    /// <summary>
    ///     Utilities used in SSL, SMTP and Smime Handeling
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        ///     CMS encryption of a data
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <param name="encryptionCertificates">A list of certificates to encrypt the data with</param>
        /// <returns>The encrypted data</returns>
        internal static byte[] Encrypt(string data, X509Certificate2Collection encryptionCertificates)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(data);
            var envelopedCms = new EnvelopedCms(new ContentInfo(messageBytes));
            var recipients = new CmsRecipientCollection(SubjectIdentifierType.IssuerAndSerialNumber,
                encryptionCertificates);
            envelopedCms.Encrypt(recipients);
            return envelopedCms.Encode();
        }

        /// <summary>
        ///     CMS (PKCS#7) signing of data
        /// </summary>
        /// <param name="data">Data to sign</param>
        /// <param name="signingCertificate">Certificate used for signing of message</param>
        /// <param name="encryptionCertificate">This certificate will be added to the end of signed text's certificate collection</param>
        /// <returns>signature</returns>
        internal static byte[] Sign(string data, X509Certificate2 signingCertificate,
            X509Certificate2 encryptionCertificate)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(data);

            var signedCms = new SignedCms(new ContentInfo(messageBytes), true);

            var cmsSigner = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, signingCertificate)
            {
                IncludeOption = X509IncludeOption.WholeChain
            };

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
        ///     Generate a random boundry for mail message
        /// </summary>
        /// <returns>A GUID based Boundry</returns>
        internal static string GenerateBoundary()
        {
            return "--CPI=_" + Guid.NewGuid();
        }


        /// <summary>
        ///     Read an <see cref="System.Net.Mail.Attachment" />Attachment and return the value of it in byte arrays.
        /// </summary>
        /// <param name="attachment">Attachment</param>
        /// <returns>byte array of attachment which is mainly used in encyption of it</returns>
        public static byte[] ReadAttachment(Attachment attachment)
        {
            var reader = new BinaryReader(attachment.ContentStream);
            attachment.ContentStream.Position = 0;
            return reader.ReadBytes((int) attachment.ContentStream.Length);
        }
    }

    /// <summary>
    ///     Login type for authentication.
    /// </summary>
    public enum AuthenticationType
    {
        /// <summary>
        ///     No authentication is used.
        /// </summary>
        UseDefualtCridentials = -1,

        /// <summary>
        ///     Base64 authentication is used.
        /// </summary>
        Base64 = 0,

        /// <summary>
        ///     PlainText text authentication is used.
        /// </summary>
        PlainText = 1
    }


    /// <summary>
    ///     List of SMTP reply codes.
    /// </summary>
    internal enum SmtpResponseCodes
    {
        SystemStatus = 211,
        Help = 214,
        Ready = 220,
        ClosingChannel = 221,
        AuthenticationSuccessfull = 235,
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
        MailNotAccepted = 521,
        AuthenticationFailed = 535,
        NotImplemented = 550,
        UserNotLocalBad = 551,
        ExceededStorage = 552,
        MailBoxNameNotValid = 553,
        TransactionFailed = 554
    }
}