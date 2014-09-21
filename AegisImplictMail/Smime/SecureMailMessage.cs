using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using net.scan.ace.SecureMail;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Represents an e-mail message.
    /// </summary>
    public class SecureMailMessage : IDisposable
    {
        # region Constructors

        /// <summary>
        /// Initializes an empty instance of the SecureMailMessage class.
        /// </summary>
        public SecureMailMessage()
        {
            InternalMailMessage = new System.Net.Mail.MailMessage();
            Attachments = new SecureAttachmentCollection();
            To = new SecureMailAddressCollection();
            CC = new SecureMailAddressCollection();
            Bcc = new SecureMailAddressCollection();
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailMessage class.
        /// </summary>
        /// <param name="from">The address of the sender of the e-mail message.</param>
        /// <param name="to">The addressses of the recipients of the e-mail message.</param>
        public SecureMailMessage(string from, string to)
            : this()
        {
            From = new SecureMailAddress(from);
            To.Add(to);
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailMessage class.
        /// </summary>
        /// <param name="from">The address of the sender of the e-mail message.</param>
        /// <param name="to">The address of the recipient of the e-mail message.</param>
        public SecureMailMessage(SecureMailAddress from, SecureMailAddress to)
            : this()
        {
            From = from;
            To.Add(to);
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailMessage class.
        /// </summary>
        /// <param name="from">The address of the sender of the e-mail message.</param>
        /// <param name="to">The addresses of the recipients of the e-mail message.</param>
        /// <param name="subject">The subject text of the e-mail message.</param>
        /// <param name="body">The body of the e-mail message.</param>
        public SecureMailMessage(string from, string to, string subject, string body)
            : this()
        {
            From = new SecureMailAddress(from);
            To.Add(to);
            InternalMailMessage.Subject = subject;
            InternalMailMessage.Body = body;
        }

        # endregion

        # region Properties

        /// <summary>
        /// Gets a list of the message's attachments.
        /// </summary>
        public SecureAttachmentCollection Attachments
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list of the addresses to be blind copied.
        /// </summary>
        public SecureMailAddressCollection Bcc
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the body of the e-mail message.
        /// </summary>
        public string Body
        {
            get
            {
                return InternalMailMessage.Body;
            }
            set
            {
                InternalMailMessage.Body = value;
            }
        }

        /// <summary>
        /// Gets or sets the encoding of the body.
        /// </summary>
        public Encoding BodyEncoding
        {
            get
            {
                return InternalMailMessage.BodyEncoding;
            }
            set
            {
                InternalMailMessage.BodyEncoding = value;
            }
        }

        /// <summary>
        /// Gets a list of addresses to be CC'd.
        /// </summary>
        public SecureMailAddressCollection CC
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the message's notification options.
        /// </summary>
        public SecureDeliveryNotificationOptions DeliveryNotificationOptions
        {
            get
            {
                return (SecureDeliveryNotificationOptions)InternalMailMessage.DeliveryNotificationOptions;
            }
            set
            {
                InternalMailMessage.DeliveryNotificationOptions = (System.Net.Mail.DeliveryNotificationOptions)value;
            }
        }

        /// <summary>
        /// Gets or sets the address that the message will be sent from.
        /// </summary>
        public SecureMailAddress From
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a list of the headers of the message.
        /// </summary>
        public NameValueCollection Headers
        {
            get
            {
                return InternalMailMessage.Headers;
            }
        }

        /// <summary>
        /// Gets or sets whether the message body should be interpreted as HTML.
        /// </summary>
        public bool IsBodyHtml
        {
            get
            {
                return InternalMailMessage.IsBodyHtml;
            }
            set
            {
                InternalMailMessage.IsBodyHtml = value;
            }
        }

        /// <summary>
        /// Gets or sets the priority of the message.
        /// </summary>
        public SecureMailPriority Priority
        {
            get
            {
                switch (InternalMailMessage.Priority)
                {
                    case System.Net.Mail.MailPriority.Normal:
                        return SecureMailPriority.Normal;
                    case System.Net.Mail.MailPriority.Low:
                        return SecureMailPriority.Low;
                    case System.Net.Mail.MailPriority.High:
                        return SecureMailPriority.High;
                    default:
                        return (SecureMailPriority)InternalMailMessage.Priority;
                }
            }
            set
            {
                switch (value)
                {
                    case SecureMailPriority.Normal:
                        InternalMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                        break;
                    case SecureMailPriority.Low:
                        InternalMailMessage.Priority = System.Net.Mail.MailPriority.Low;
                        break;
                    case SecureMailPriority.High:
                        InternalMailMessage.Priority = System.Net.Mail.MailPriority.High;
                        break;
                    default:
                        InternalMailMessage.Priority = (System.Net.Mail.MailPriority)value;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the message's reply-to address.
        /// </summary>
        public SecureMailAddress ReplyTo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address of the sender of the message.
        /// </summary>
        public SecureMailAddress Sender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        public string Subject
        {
            get
            {
                return InternalMailMessage.Subject;
            }
            set
            {
                InternalMailMessage.Subject = value;
            }
        }

        /// <summary>
        /// Gets or sets the encoding of the message subject.
        /// </summary>
        public Encoding SubjectEncoding
        {
            get
            {
                return InternalMailMessage.SubjectEncoding;
            }
            set
            {
                InternalMailMessage.SubjectEncoding = value;
            }
        }

        /// <summary>
        /// Gets a list the recipients of the e-mail message.
        /// </summary>
        public SecureMailAddressCollection To
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the message is a multipart MIME message.
        /// </summary>
        internal bool IsMultipart
        {
            get
            {
                return !IsEncrypted && (Attachments.Count > 0 || IsSigned);
            }
        }

        /// <summary>
        /// Gets or sets whether the message should include a cryptographic signature.
        /// </summary>
        public bool IsSigned
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the message should be encrypted.
        /// </summary>
        public bool IsEncrypted
        {
            get;
            set;
        }

        internal System.Net.Mail.MailMessage InternalMailMessage
        {
            get;
            private set;
        }

        # endregion

        # region Methods


        private SecureMessageContent GetUnsignedContent()
        {

            SecureContentType bodyType = new SecureContentType();

            bodyType.MediaType = this.IsBodyHtml ? "text/html" : "text/plain";

            bodyType.CharSet = BodyEncoding.BodyName;

            SecureTransferEncoding bodyTransferEncoding;

            Encoding bodyEncoding = BodyEncoding ?? Encoding.ASCII;

            if (bodyEncoding == Encoding.ASCII || bodyEncoding == Encoding.UTF8)
            {
                bodyTransferEncoding = SecureTransferEncoding.QuotedPrintable;
            }
            else
            {
                bodyTransferEncoding = SecureTransferEncoding.Base64;
            }


            SecureMessageContent bodyContent = new SecureMessageContent(
                bodyEncoding.GetBytes(this.Body),
                bodyType,
                bodyTransferEncoding,
                IsMultipart || IsEncrypted);

            if (this.Attachments.Count == 0)
            {
                return bodyContent;
            }
            else
            {
                SecureContentType bodyWithAttachmentsType = new SecureContentType("multipart/mixed");
                bodyWithAttachmentsType.GenerateBoundary();

                StringBuilder message = new StringBuilder();
                message.Append("\r\n");
                message.Append("--");
                message.Append(bodyWithAttachmentsType.Boundary);
                message.Append("\r\n");
                message.Append("Content-Type: ");
                message.Append(bodyContent.ContentType.ToString());
                message.Append("\r\n");
                message.Append("Content-Transfer-Encoding: ");
                message.Append(TransferEncoder.GetTransferEncodingName(bodyContent.TransferEncoding));
                message.Append("\r\n\r\n");
                message.Append(Encoding.ASCII.GetString(bodyContent.Body));
                message.Append("\r\n");

                foreach (SecureAttachment attachment in Attachments)
                {
                    message.Append("--");
                    message.Append(bodyWithAttachmentsType.Boundary);
                    message.Append("\r\n");
                    message.Append("Content-Type: ");
                    message.Append(attachment.ContentType.ToString());
                    message.Append("\r\n");
                    message.Append("Content-Transfer-Encoding: base64\r\n\r\n");
                    message.Append(TransferEncoder.ToBase64(attachment.RawBytes));
                    message.Append("\r\n\r\n");
                }

                message.Append("--");
                message.Append(bodyWithAttachmentsType.Boundary);
                message.Append("--\r\n");

                return new SecureMessageContent(Encoding.ASCII.GetBytes(message.ToString()), bodyWithAttachmentsType, SecureTransferEncoding.SevenBit, false);

            }
        }

        private SecureMessageContent SignContent(SecureMessageContent unsignedContent)
        {
            if (From.SigningCertificate == null)
            {
                throw new InvalidOperationException("Can't sign message unless the From property contains a signing certificate.");
            }

            SecureContentType signedContentType = new SecureContentType("multipart/signed; protocol=\"application/x-pkcs7-signature\"; micalg=SHA1; ");
            signedContentType.GenerateBoundary();

            StringBuilder fullUnsignedMessageBuilder = new StringBuilder();
            fullUnsignedMessageBuilder.Append("Content-Type: ");
            fullUnsignedMessageBuilder.Append(unsignedContent.ContentType.ToString());
            fullUnsignedMessageBuilder.Append("\r\n");
            fullUnsignedMessageBuilder.Append("Content-Transfer-Encoding: ");
            fullUnsignedMessageBuilder.Append(TransferEncoder.GetTransferEncodingName(unsignedContent.TransferEncoding));
            fullUnsignedMessageBuilder.Append("\r\n\r\n");
            fullUnsignedMessageBuilder.Append(Encoding.ASCII.GetString(unsignedContent.Body));

            string fullUnsignedMessage = fullUnsignedMessageBuilder.ToString();

            byte[] signature = CryptoHelper.GetSignature(fullUnsignedMessage, From.SigningCertificate, From.EncryptionCertificate);

            StringBuilder signedMessageBuilder = new StringBuilder();

            signedMessageBuilder.Append("--");
            signedMessageBuilder.Append(signedContentType.Boundary);
            signedMessageBuilder.Append("\r\n");
            signedMessageBuilder.Append(fullUnsignedMessage);
            signedMessageBuilder.Append("\r\n");
            signedMessageBuilder.Append("--");
            signedMessageBuilder.Append(signedContentType.Boundary);
            signedMessageBuilder.Append("\r\n");
            signedMessageBuilder.Append("Content-Type: application/x-pkcs7-signature; name=\"smime.p7s\"\r\n");
            signedMessageBuilder.Append("Content-Transfer-Encoding: base64\r\n");
            signedMessageBuilder.Append("Content-Disposition: attachment; filename=\"smime.p7s\"\r\n\r\n");
            signedMessageBuilder.Append(TransferEncoder.ToBase64(signature));
            signedMessageBuilder.Append("\r\n\r\n");
            signedMessageBuilder.Append("--");
            signedMessageBuilder.Append(signedContentType.Boundary);
            signedMessageBuilder.Append("--\r\n");

            return new SecureMessageContent(Encoding.ASCII.GetBytes(
                signedMessageBuilder.ToString()),
                signedContentType,
                SecureTransferEncoding.SevenBit,
                false);
        }

        private SecureMessageContent EncryptContent(SecureMessageContent unencryptedContent)
        {
            X509Certificate2Collection encryptionCertificates = new X509Certificate2Collection();

            # region Gather All Encryption Certificates

            if (From.EncryptionCertificate == null)
            {
                throw new InvalidOperationException("To send an encrypted message, the sender must have an encryption certificate specified.");
            }
            else
            {
                encryptionCertificates.Add(From.EncryptionCertificate);
            }

            foreach (IEnumerable<SecureMailAddress> addressList in new IEnumerable<SecureMailAddress>[] { To, CC, Bcc })
            {
                foreach (SecureMailAddress address in addressList)
                {
                    if (address.EncryptionCertificate == null)
                    {
                        throw new InvalidOperationException("To send an encrypted message, all receivers (To, CC, and Bcc) must have an encryption certificate specified.");
                    }
                    else
                    {
                        encryptionCertificates.Add(address.EncryptionCertificate);
                    }
                }
            }

            # endregion

            SecureContentType encryptedContentType = new SecureContentType("application/x-pkcs7-mime; smime-type=enveloped-data; name=\"smime.p7m\"");

            StringBuilder fullUnencryptedMessageBuilder = new StringBuilder();
            fullUnencryptedMessageBuilder.Append("Content-Type: ");
            fullUnencryptedMessageBuilder.Append(unencryptedContent.ContentType.ToString());
            fullUnencryptedMessageBuilder.Append("\r\n");
            fullUnencryptedMessageBuilder.Append("Content-Transfer-Encoding: ");
            fullUnencryptedMessageBuilder.Append(TransferEncoder.GetTransferEncodingName(unencryptedContent.TransferEncoding));
            fullUnencryptedMessageBuilder.Append("\r\n\r\n");
            fullUnencryptedMessageBuilder.Append(Encoding.ASCII.GetString(unencryptedContent.Body));

            string fullUnencryptedMessage = fullUnencryptedMessageBuilder.ToString();

            byte[] encryptedBytes = CryptoHelper.EncryptMessage(fullUnencryptedMessage, encryptionCertificates);

            return new SecureMessageContent(encryptedBytes,
                encryptedContentType,
                SecureTransferEncoding.Base64,
                false);
        }

        internal SecureMessageContent GetCompleteContent()
        {
            SecureMessageContent returnValue = GetUnsignedContent();

            if (IsSigned)
            {
                returnValue = SignContent(returnValue);
            }

            if (IsEncrypted)
            {
                returnValue = EncryptContent(returnValue);
            }

            return returnValue;

        }

        /// <summary>
        /// Converts the message to a System.Net.Mail.MailMessage instance.
        /// </summary>
        /// <returns>A System.Net.Mail.MailMessage instance.</returns>
        public System.Net.Mail.MailMessage ToMailMessage()
        {
            System.Net.Mail.MailMessage returnValue = new System.Net.Mail.MailMessage();

            if (From != null)
            {
                returnValue.From = From;
            }

            if (Sender != null)
            {
                returnValue.Sender = Sender;
            }

            if (ReplyTo != null)
            {
                returnValue.ReplyTo = ReplyTo;
            }


            foreach (SecureMailAddress toAddress in To)
            {
                returnValue.To.Add(toAddress);
            }

            foreach (SecureMailAddress ccAddress in CC)
            {
                returnValue.CC.Add(ccAddress);
            }

            foreach (SecureMailAddress bccAddress in Bcc)
            {
                returnValue.Bcc.Add(bccAddress);
            }

            returnValue.DeliveryNotificationOptions = InternalMailMessage.DeliveryNotificationOptions;

            foreach (string header in InternalMailMessage.Headers)
            {
                returnValue.Headers.Add(header, InternalMailMessage.Headers[header]);
            }

            returnValue.Priority = InternalMailMessage.Priority;
            returnValue.Subject = InternalMailMessage.Subject;
            returnValue.SubjectEncoding = InternalMailMessage.SubjectEncoding;

            SecureMessageContent content = GetCompleteContent();

            MemoryStream contentStream = new MemoryStream();

            if (this.IsMultipart)
            {
                byte[] mimeMessage = Encoding.ASCII.GetBytes("This is a multi-part message in MIME format.\r\n\r\n");

                contentStream.Write(mimeMessage, 0, mimeMessage.Length);
            }

            byte[] encodedBody;

            switch (content.TransferEncoding)
            {
                case SecureTransferEncoding.SevenBit:
                    encodedBody = Encoding.ASCII.GetBytes(Regex.Replace(Encoding.ASCII.GetString(content.Body), "^\\.", "..", RegexOptions.Multiline));
                    break;
                default:
                    encodedBody = content.Body;
                    break;
            }

            contentStream.Write(encodedBody, 0, encodedBody.Length);

            contentStream.Position = 0;

            System.Net.Mail.AlternateView contentView = new System.Net.Mail.AlternateView(contentStream, content.ContentType.InternalContentType);
            contentView.TransferEncoding = TransferEncoder.ConvertTransferEncoding(content.TransferEncoding);

            returnValue.AlternateViews.Add(contentView);

            return returnValue;
        }

        # endregion

        # region Overloaded Operators

        public static implicit operator System.Net.Mail.MailMessage(SecureMailMessage message)
        {
            if (message == null)
            {
                return null;
            }
            else
            {
                return message.ToMailMessage();
            }
        }

        # endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the object, freeing all resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes the object, freeing all resources.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (InternalMailMessage != null)
                {
                    InternalMailMessage.Dispose();
                }
            }
        }

        #endregion
    }
}
