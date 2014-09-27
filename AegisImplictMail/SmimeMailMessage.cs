using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AegisImplicitMail
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
       //    InternalMailMessage = new System.Net.Mail.MailMessage();
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
        public SmimeMailMessage(string from, string to, string subject, string body):base(from,to,subject,body)
        {
            From = new SmimeMailAddress(from);
            To.Add(to);
        }

        # endregion

        # region Properties
      
        /// <summary>
        /// Gets a list of the addresses to be blind copied.
        /// </summary>
        public new SmimeMailAddressCollection Bcc
        {
            get;
            private set;
        }

      

        /// <summary>
        /// Gets a list of addresses to be CC'd.
        /// </summary>
        public new SmimeMailAddressCollection CC
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the address that the message will be sent from.
        /// </summary>
        public new SmimeMailAddress From
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the address of the sender of the message.
        /// </summary>
        public new SmimeMailAddress Sender
        { private get;
            set;
        }

        /// <summary>
        /// Gets a list the recipients of the e-mail message.
        /// </summary>
        public new SmimeMailAddressCollection To { get; private set; }

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
        # endregion

        # region Methods


        private SmimeMessageContent GetUnsignedContent()
        {

            var bodyType = new ContentType
            {
                MediaType = IsBodyHtml ? "text/html" : "text/plain",
                CharSet = BodyEncoding.BodyName
            };

            TransferEncoding bodyTransferEncoding;

            var bodyEncoding = BodyEncoding ?? Encoding.ASCII;

            if (bodyEncoding == Encoding.ASCII || bodyEncoding == Encoding.UTF8)
            {
                bodyTransferEncoding = TransferEncoding.QuotedPrintable;
            }
            else
            {
                bodyTransferEncoding = TransferEncoding.Base64;
            }


            var bodyContent = new SmimeMessageContent(
                bodyEncoding.GetBytes(Body),
                bodyType,
                bodyTransferEncoding,
                IsMultipart || IsEncrypted);

            if (Attachments.Count == 0)
            {
                return bodyContent;
            }
            var bodyWithAttachmentsType = new ContentType("multipart/mixed") {Boundary = Helpers.GenerateBoundary()};

            var message = new StringBuilder();
            message.Append("\r\n");
            message.Append("--");
            message.Append(bodyWithAttachmentsType.Boundary);
            message.Append("\r\n");
            message.Append("Content-Type: ");
            message.Append(bodyContent.ContentType);
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
                message.Append(attachment.ContentType);
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

        /// <summary>
        /// Sign the message, It convert mail message to smime
        /// </summary>
        /// <param name="unsignedContent">Original message</param>
        /// <returns>Smime</returns>
        private SmimeMessageContent SignContent(SmimeMessageContent unsignedContent)
        {
            if (From.SigningCertificate == null)
            {
                throw new InvalidOperationException("Can't sign message unless the From property contains a signing certificate.");
            }

            var signedContentType = new ContentType("multipart/signed; protocol=\"application/x-pkcs7-signature\"; micalg=SHA1; ")
            {
                Boundary = Helpers.GenerateBoundary()
            };

            var unsignedStringBuilder = new StringBuilder();
            unsignedStringBuilder.Append("Content-Type: ");
            unsignedStringBuilder.Append(unsignedContent.ContentType);
            unsignedStringBuilder.Append("\r\n");
            unsignedStringBuilder.Append("Content-Transfer-Encoding: ");
            unsignedStringBuilder.Append(TransferEncoder.GetTransferEncodingName(unsignedContent.TransferEncoding));
            unsignedStringBuilder.Append("\r\n\r\n");
            unsignedStringBuilder.Append(Encoding.ASCII.GetString(unsignedContent.Body));

            string fullUnsignedMessage = unsignedStringBuilder.ToString();

            byte[] signature = Helpers.Sign(fullUnsignedMessage, From.SigningCertificate, From.EncryptionCertificate);

            var signedStringBuilder = new StringBuilder();

            signedStringBuilder.Append("--");
            signedStringBuilder.Append(signedContentType.Boundary);
            signedStringBuilder.Append("\r\n");
            signedStringBuilder.Append(fullUnsignedMessage);
            signedStringBuilder.Append("\r\n");
            signedStringBuilder.Append("--");
            signedStringBuilder.Append(signedContentType.Boundary);
            signedStringBuilder.Append("\r\n");
            signedStringBuilder.Append("Content-Type: application/x-pkcs7-signature; name=\"smime.p7s\"\r\n");
            signedStringBuilder.Append("Content-Transfer-Encoding: base64\r\n");
            signedStringBuilder.Append("Content-Disposition: attachment; filename=\"smime.p7s\"\r\n\r\n");
            signedStringBuilder.Append(TransferEncoder.ToBase64(signature));
            signedStringBuilder.Append("\r\n\r\n");
            signedStringBuilder.Append("--");
            signedStringBuilder.Append(signedContentType.Boundary);
            signedStringBuilder.Append("--\r\n");

            return new SmimeMessageContent(Encoding.ASCII.GetBytes(
                signedStringBuilder.ToString()),
                signedContentType,
                TransferEncoding.SevenBit,
                false);
        }

        private SmimeMessageContent EncryptContent(SmimeMessageContent unencryptedContent)
        {
            var encryptionCertificates = new X509Certificate2Collection();


            if (From.EncryptionCertificate == null)
            {
                throw new InvalidOperationException("To send an encrypted message, the sender must have an encryption certificate specified.");
            }
            encryptionCertificates.Add(From.EncryptionCertificate);

            foreach (IEnumerable<SmimeMailAddress> addressList in new IEnumerable<SmimeMailAddress>[] { To, CC, Bcc })
            {
                foreach (SmimeMailAddress address in addressList)
                {
                    if (address.EncryptionCertificate == null)
                    {
                        throw new InvalidOperationException("To send an encrypted message, all receivers (To, CC, and Bcc) must have an encryption certificate specified.");
                    }
                        encryptionCertificates.Add(address.EncryptionCertificate);
                }
            }


            var encryptedContentType = new ContentType("application/x-pkcs7-mime; smime-type=enveloped-data; name=\"smime.p7m\"");

            var fullUnencryptedMessageBuilder = new StringBuilder();
            fullUnencryptedMessageBuilder.Append("Content-Type: ");
            fullUnencryptedMessageBuilder.Append(unencryptedContent.ContentType);
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
        /// You can use this function to send explicit and normal S/Mime Messages. It Converts the message to a System.Net.Mail.MailMessage instance.
        /// </summary>
        /// <returns>A System.Net.Mail.MailMessage instance.</returns>
        public MailMessage ToMailMessage()
        {
            var result = new MailMessage();
            #region Set Addressed
            if (From != null)
            {
                result.From = From;
            }
            if (Sender != null)
            {
                result.Sender = Sender;
            }
            if (ReplyToList.Count >0)
            {
                foreach (var variable in ReplyToList)
                {
                    result.ReplyToList.Add(variable);
                }
            }

            foreach (SmimeMailAddress toAddress in To)
            {
                result.To.Add(toAddress);
            }

            foreach (SmimeMailAddress ccAddress in CC)
            {
                result.CC.Add(ccAddress);
            }

            foreach (SmimeMailAddress bccAddress in Bcc)
            {
                result.Bcc.Add(bccAddress);
            }
            #endregion

            result.DeliveryNotificationOptions = DeliveryNotificationOptions;

            foreach (string header in Headers)
            {
                result.Headers.Add(header, Headers[header]);
            }

            result.Priority = Priority;
            result.Subject = Subject;
            result.SubjectEncoding = SubjectEncoding;

            //Generate Signe/Encrypted content
            SmimeMessageContent smimeContent = GetCompleteContent();

            var contentStream = new MemoryStream();

            if (IsMultipart)
            {
                byte[] mimeMessage = Encoding.ASCII.GetBytes("This is a multi-part message in MIME format.\r\n\r\n");

                contentStream.Write(mimeMessage, 0, mimeMessage.Length);
            }

            byte[] encodedBody;

            //Generate body of message and return
            switch (smimeContent.TransferEncoding)
            {
                case TransferEncoding.SevenBit:
                    encodedBody = Encoding.ASCII.GetBytes(Regex.Replace(Encoding.ASCII.GetString(smimeContent.Body), "^\\.", "..", RegexOptions.Multiline));
                    break;
                default:
                    encodedBody = smimeContent.Body;
                    break;
            }

            contentStream.Write(encodedBody, 0, encodedBody.Length);

            contentStream.Position = 0;

            var contentView = new AlternateView(contentStream, smimeContent.ContentType)
            {
                TransferEncoding = TransferEncoder.ConvertTransferEncoding(smimeContent.TransferEncoding)
            };

            result.AlternateViews.Add(contentView);

            return result;
        }



        # endregion

        # region Convertor Operators
        /// <summary>
        /// Converts the message to a System.Net.Mail.MailMessage instance.
        /// </summary>
        /// <returns>A System.Net.Mail.MailMessage instance.</returns>
        private MimeMailMessage ToSmtpMailMessage()
        {
            var returnValue = new MimeMailMessage();
            foreach (var attachment1 in Attachments)
            {
                var attachment = (MimeAttachment) attachment1;
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

            if (ReplyToList.Count > 0)
            {
                foreach (var variable in ReplyToList)
                {
                    returnValue.ReplyToList.Add(variable);
                }
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

            returnValue.DeliveryNotificationOptions = DeliveryNotificationOptions;

            foreach (string header in Headers)
            {
                returnValue.Headers.Add(header, Headers[header]);
            }

            returnValue.Priority = Priority;
            returnValue.Subject = Subject;
            returnValue.SubjectEncoding = SubjectEncoding;

            SmimeMessageContent content = GetCompleteContent();

            var contentStream = new MemoryStream();

            if (IsMultipart)
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

            var contentView = new AlternateView(contentStream, content.ContentType)
            {
                TransferEncoding = TransferEncoder.ConvertTransferEncoding(content.TransferEncoding)
            };

            returnValue.AlternateViews.Add(contentView);

            return returnValue;
        }

        /// <summary>
        /// To cast To normal Mail Message
        /// </summary>
        /// <param name="message">A Smime mail message</param>
        /// <returns>SmtpMailMessage equalance of the SmimeMessage</returns>
        public static implicit operator MimeMailMessage(SmimeMailMessage message)
        {
            if (message == null)
            {
                return null;
            }
            return message.ToSmtpMailMessage();
        }

        # endregion

        #region IDisposable Members


        /*
        /// <summary>
        /// Disposes the object, freeing all resources.
        /// </summary>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (InternalMailMessage != null)
                {
                    Dispose();
                }
            }
        }
        */
        #endregion
    }
}
