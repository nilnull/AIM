using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace SslImplicitMail
{
    /// <summary>
    /// Represents an e-mail message.
    /// </summary>
    public sealed class SmimeMailMessage : AbstractMailMessage, IDisposable
    {
        # region Constructors

        /// <summary>
        /// Initializes an empty instance of the SecureMailMessage class.
        /// </summary>
        public SmimeMailMessage()
        {
            InternalMailMessage = new System.Net.Mail.MailMessage();
            To = new SmimeMailAddressCollection();
            CC = new SmimeMailAddressCollection();
            Bcc = new SmimeMailAddressCollection();
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailMessage class.
        /// </summary>
        /// <param name="from">The address of the sender of the e-mail message.</param>
        /// <param name="to">The addressses of the recipients of the e-mail message.</param>
        public SmimeMailMessage(string from, string to)
            : this()
        {
            From = new SmimeMailAddress(from);
            To.Add(to);
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailMessage class.
        /// </summary>
        /// <param name="from">The address of the sender of the e-mail message.</param>
        /// <param name="to">The address of the recipient of the e-mail message.</param>
        public SmimeMailMessage(SmimeMailAddress from, SmimeMailAddress to)
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
        public SmimeMailMessage(string from, string to, string subject, string body)
            : this()
        {
            From = new SmimeMailAddress(from);
            To.Add(to);
            InternalMailMessage.Subject = subject;
            InternalMailMessage.Body = body;
        }

        # endregion

        # region Properties

        /// <summary>
        /// Gets a list of the message's attachments.
        /// </summary>
        public AttachmentCollection Attachments
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a list of the addresses to be blind copied.
        /// </summary>
        public SmimeMailAddressCollection Bcc
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
        public SmimeMailAddressCollection CC
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the message's notification options.
        /// </summary>
        public System.Net.Mail.DeliveryNotificationOptions DeliveryNotificationOptions
        {
            get
            {
                return InternalMailMessage.DeliveryNotificationOptions;
            }
            set
            {
                InternalMailMessage.DeliveryNotificationOptions = value;
            }
        }

        /// <summary>
        /// Gets or sets the address that the message will be sent from.
        /// </summary>
        public SmimeMailAddress From
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
        public MailPriority Priority
        {
            get
            {
                switch (InternalMailMessage.Priority)
                {
                    case System.Net.Mail.MailPriority.Normal:
                        return MailPriority.Normal;
                    case System.Net.Mail.MailPriority.Low:
                        return MailPriority.Low;
                    case System.Net.Mail.MailPriority.High:
                        return MailPriority.High;
                    default:
                        return (MailPriority)InternalMailMessage.Priority;
                }
            }
            set
            {
                switch (value)
                {
                    case MailPriority.Normal:
                        InternalMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                        break;
                    case MailPriority.Low:
                        InternalMailMessage.Priority = System.Net.Mail.MailPriority.Low;
                        break;
                    case MailPriority.High:
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
        public SmimeMailAddress ReplyTo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address of the sender of the message.
        /// </summary>
        public SmimeMailAddress Sender
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
        public SmimeMailAddressCollection To
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


        private SmimeMessageContent GetUnsignedContent()
        {

            ContentType bodyType = new ContentType();

            bodyType.MediaType = this.IsBodyHtml ? "text/html" : "text/plain";

            bodyType.CharSet = BodyEncoding.BodyName;

            TransferEncoding bodyTransferEncoding;

            Encoding bodyEncoding = BodyEncoding ?? Encoding.ASCII;

            if (bodyEncoding == Encoding.ASCII || bodyEncoding == Encoding.UTF8)
            {
                bodyTransferEncoding = TransferEncoding.QuotedPrintable;
            }
            else
            {
                bodyTransferEncoding = TransferEncoding.Base64;
            }


            SmimeMessageContent bodyContent = new SmimeMessageContent(
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
                ContentType bodyWithAttachmentsType = new ContentType("multipart/mixed");
                bodyWithAttachmentsType.Boundary = Helpers.GenerateBoundary();

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

                foreach (Attachment attachment in Attachments)
                {
                    message.Append("--");
                    message.Append(bodyWithAttachmentsType.Boundary);
                    message.Append("\r\n");
                    message.Append("Content-Type: ");
                    message.Append(attachment.ContentType.ToString());
                    message.Append("\r\n");
                    message.Append("Content-Transfer-Encoding: base64\r\n\r\n");
                    message.Append(TransferEncoder.ToBase64(Helpers.ReadAttachment(attachment)));
                    message.Append("\r\n\r\n");
                }

                message.Append("--");
                message.Append(bodyWithAttachmentsType.Boundary);
                message.Append("--\r\n");

                return new SmimeMessageContent(Encoding.ASCII.GetBytes(message.ToString()), bodyWithAttachmentsType, TransferEncoding.SevenBit, false);

            }
        }

        private SmimeMessageContent SignContent(SmimeMessageContent unsignedContent)
        {
            if (From.SigningCertificate == null)
            {
                throw new InvalidOperationException("Can't sign message unless the From property contains a signing certificate.");
            }

            ContentType signedContentType = new ContentType("multipart/signed; protocol=\"application/x-pkcs7-signature\"; micalg=SHA1; ");
            signedContentType.Boundary = Helpers.GenerateBoundary();

            StringBuilder fullUnsignedMessageBuilder = new StringBuilder();
            fullUnsignedMessageBuilder.Append("Content-Type: ");
            fullUnsignedMessageBuilder.Append(unsignedContent.ContentType.ToString());
            fullUnsignedMessageBuilder.Append("\r\n");
            fullUnsignedMessageBuilder.Append("Content-Transfer-Encoding: ");
            fullUnsignedMessageBuilder.Append(TransferEncoder.GetTransferEncodingName(unsignedContent.TransferEncoding));
            fullUnsignedMessageBuilder.Append("\r\n\r\n");
            fullUnsignedMessageBuilder.Append(Encoding.ASCII.GetString(unsignedContent.Body));

            string fullUnsignedMessage = fullUnsignedMessageBuilder.ToString();

            byte[] signature = Helpers.Sign(fullUnsignedMessage, From.SigningCertificate, From.EncryptionCertificate);

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

            return new SmimeMessageContent(Encoding.ASCII.GetBytes(
                signedMessageBuilder.ToString()),
                signedContentType,
                TransferEncoding.SevenBit,
                false);
        }

        private SmimeMessageContent EncryptContent(SmimeMessageContent unencryptedContent)
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

            foreach (IEnumerable<SmimeMailAddress> addressList in new IEnumerable<SmimeMailAddress>[] { To, CC, Bcc })
            {
                foreach (SmimeMailAddress address in addressList)
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

            ContentType encryptedContentType = new ContentType("application/x-pkcs7-mime; smime-type=enveloped-data; name=\"smime.p7m\"");

            StringBuilder fullUnencryptedMessageBuilder = new StringBuilder();
            fullUnencryptedMessageBuilder.Append("Content-Type: ");
            fullUnencryptedMessageBuilder.Append(unencryptedContent.ContentType.ToString());
            fullUnencryptedMessageBuilder.Append("\r\n");
            fullUnencryptedMessageBuilder.Append("Content-Transfer-Encoding: ");
            fullUnencryptedMessageBuilder.Append(TransferEncoder.GetTransferEncodingName(unencryptedContent.TransferEncoding));
            fullUnencryptedMessageBuilder.Append("\r\n\r\n");
            fullUnencryptedMessageBuilder.Append(Encoding.ASCII.GetString(unencryptedContent.Body));

            string fullUnencryptedMessage = fullUnencryptedMessageBuilder.ToString();

            byte[] encryptedBytes = Helpers.Encrypt(fullUnencryptedMessage, encryptionCertificates);

            return new SmimeMessageContent(encryptedBytes,
                encryptedContentType,
                TransferEncoding.Base64,
                false);
        }

        internal SmimeMessageContent GetCompleteContent()
        {
            SmimeMessageContent returnValue = GetUnsignedContent();

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


            foreach (SmimeMailAddress toAddress in To)
            {
                returnValue.To.Add(toAddress);
            }

            foreach (SmimeMailAddress ccAddress in CC)
            {
                returnValue.CC.Add(ccAddress);
            }

            foreach (SmimeMailAddress bccAddress in Bcc)
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

            SmimeMessageContent content = GetCompleteContent();

            MemoryStream contentStream = new MemoryStream();

            if (this.IsMultipart)
            {
                byte[] mimeMessage = Encoding.ASCII.GetBytes("This is a multi-part message in MIME format.\r\n\r\n");

                contentStream.Write(mimeMessage, 0, mimeMessage.Length);
            }

            byte[] encodedBody;

            switch (content.TransferEncoding)
            {
                case TransferEncoding.SevenBit:
                    encodedBody = Encoding.ASCII.GetBytes(Regex.Replace(Encoding.ASCII.GetString(content.Body), "^\\.", "..", RegexOptions.Multiline));
                    break;
                default:
                    encodedBody = content.Body;
                    break;
            }

            contentStream.Write(encodedBody, 0, encodedBody.Length);

            contentStream.Position = 0;

            System.Net.Mail.AlternateView contentView = new System.Net.Mail.AlternateView(contentStream, content.ContentType);
            contentView.TransferEncoding = TransferEncoder.ConvertTransferEncoding(content.TransferEncoding);

            returnValue.AlternateViews.Add(contentView);

            return returnValue;
        }


        /// <summary>
        /// Converts the message to a System.Net.Mail.MailMessage instance.
        /// </summary>
        /// <returns>A System.Net.Mail.MailMessage instance.</returns>
        public MimeMailMessage ToSmtpMailMessage()
        {
            MimeMailMessage returnValue = new MimeMailMessage();
            foreach (MimeAttachment attachment in Attachments)
            {
                returnValue.Attachments.Add(attachment);
            }
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


            foreach (SmimeMailAddress toAddress in To)
            {
                returnValue.To.Add(toAddress);
            }

            foreach (SmimeMailAddress ccAddress in CC)
            {
                returnValue.CC.Add(ccAddress);
            }

            foreach (SmimeMailAddress bccAddress in Bcc)
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

            SmimeMessageContent content = GetCompleteContent();

            MemoryStream contentStream = new MemoryStream();

            if (this.IsMultipart)
            {
                byte[] mimeMessage = Encoding.ASCII.GetBytes("This is a multi-part message in MIME format.\r\n\r\n");

                contentStream.Write(mimeMessage, 0, mimeMessage.Length);
            }

            byte[] encodedBody;

            switch (content.TransferEncoding)
            {
                case TransferEncoding.SevenBit:
                    encodedBody = Encoding.ASCII.GetBytes(Regex.Replace(Encoding.ASCII.GetString(content.Body), "^\\.", "..", RegexOptions.Multiline));
                    break;
                default:
                    encodedBody = content.Body;
                    break;
            }

            contentStream.Write(encodedBody, 0, encodedBody.Length);

            contentStream.Position = 0;

            System.Net.Mail.AlternateView contentView = new System.Net.Mail.AlternateView(contentStream, content.ContentType);
            contentView.TransferEncoding = TransferEncoder.ConvertTransferEncoding(content.TransferEncoding);

            returnValue.AlternateViews.Add(contentView);

            return returnValue;
        }


        # endregion

        # region Overloaded Operators


        public static implicit operator MimeMailMessage(SmimeMailMessage message)
        {
            if (message == null)
            {
                return null;
            }
            else
            {
                return message.ToSmtpMailMessage();
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
        private void Dispose(bool disposing)
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
