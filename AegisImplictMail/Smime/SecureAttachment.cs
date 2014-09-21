using System;
using System.IO;
using net.scan.ace.SecureMail;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Represents an attachment to an e-mail.
    /// </summary>
    public class SecureAttachment
    {
        
        # region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="fileName">A <see cref="T:System.String"></see> that contains a file path to use to create this attachment.</param>
        public SecureAttachment(string fileName)
        {
            RawBytes = File.ReadAllBytes(fileName);
            ContentType = new SecureContentType();
            ContentType.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="fileName">A <see cref="T:System.String"></see> that contains a file path to use to create this attachment.</param>
        /// <param name="mediaType"><see cref="T:System.String"></see> that contains the media type (eg: text/html) of the attachment.</param>
        public SecureAttachment(string fileName, string mediaType)
        {
            RawBytes = File.ReadAllBytes(fileName);
            ContentType = new SecureContentType(mediaType);
            ContentType.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="fileName">A <see cref="T:System.String"></see> that contains a file path to use to create this attachment.</param>
        /// <param name="contentType">A <see cref="T:net.scan.aegis.ace.SecureMail.SecureContentType"></see> object which describes the attachment's content type.</param>
        public SecureAttachment(string fileName, SecureContentType contentType)
        {
            if (contentType == null)
            {
                throw new ArgumentNullException("contentType");
            }

            RawBytes = File.ReadAllBytes(fileName);
            ContentType = contentType;
            ContentType.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentStream">A stream containing the content to attach.</param>
        /// <param name="name">The content name.</param>
        public SecureAttachment(Stream contentStream, string name)
        {
            if (contentStream == null)
            {
                throw new ArgumentNullException("contentStream");
            }

            BinaryReader reader = new BinaryReader(contentStream);

            contentStream.Position = 0;

            RawBytes = reader.ReadBytes((int)contentStream.Length);

            ContentType = new SecureContentType();
            ContentType.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentStream">A stream containing the content to attach.</param>
        /// <param name="contentType">A <see cref="T:net.scan.aegis.ace.SecureMail.SecureContentType"></see> object which describes the attachment's content type.</param>
        public SecureAttachment(Stream contentStream, SecureContentType contentType)
        {
            if (contentStream == null)
            {
                throw new ArgumentNullException("contentStream");
            }

            BinaryReader reader = new BinaryReader(contentStream);

            contentStream.Position = 0;

            RawBytes = reader.ReadBytes((int)contentStream.Length);

            ContentType = contentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentStream">A stream containing the content to attach.</param>
        /// <param name="name">The name of the content.</param>
        /// <param name="mediaType">The type of content (eg: text/html).</param>
        public SecureAttachment(Stream contentStream, string name, string mediaType)
        {
            if (contentStream == null)
            {
                throw new ArgumentNullException("contentStream");
            }

            BinaryReader reader = new BinaryReader(contentStream);

            contentStream.Position = 0;

            RawBytes = reader.ReadBytes((int)contentStream.Length);

            ContentType = new SecureContentType(mediaType);
            ContentType.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentBytes">A byte array containing the content to attach.</param>
        /// <param name="name">The name of the content.</param>
        public SecureAttachment(byte[] contentBytes, string name)
        {
            RawBytes = contentBytes;
            ContentType = new SecureContentType();
            ContentType.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentBytes">A byte array containing the content to attach.</param>
        /// <param name="contentType">A <see cref="T:net.scan.aegis.ace.SecureMail.SecureContentType"></see> object which describes the attachment's content type.</param>
        public SecureAttachment(byte[] contentBytes, SecureContentType contentType)
        {
            RawBytes = contentBytes;
            ContentType = contentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:net.scan.aegis.ace.SecureMail.SecureAttachment"></see> class with the specified content. 
        /// </summary>
        /// <param name="contentBytes">A byte array containing the content to attach.</param>
        /// <param name="name">The name of the content.</param>
        /// <param name="mediaType">The type of content (eg: text/html).</param>
        public SecureAttachment(byte[] contentBytes, string name, string mediaType)
        {
            RawBytes = contentBytes;
            ContentType = new SecureContentType(mediaType);
            ContentType.Name = name;
        }

        # endregion

        # region Properties

        /// <summary>
        /// Gets a byte array representing the raw content of the attachment.
        /// </summary>
        internal byte[] RawBytes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the content type of the attachment.
        /// </summary>
        public SecureContentType ContentType
        {
            get;
            private set;
        }

        # endregion
    }
}
