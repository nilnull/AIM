using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace AegisImplicitMail
{

    /// <summary>
    /// Class for holding an attachment's information.
    /// </summary>
    public class MimeAttachment : Attachment
    {
        #region properties
        internal static int InlineCount;
        internal static int AttachCount;
        private AttachmentLocation _location;
        /// <summary>
        /// File to send.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Content type of file (application/octet-stream for regular attachments).
        /// </summary>
        //		public ContentType ContentType;

        /// <summary>
        /// Where to put the attachment.
        /// </summary>
        public AttachmentLocation Location
        {
            get { return _location; }
            set
            {
                if (value == AttachmentLocation.Attachmed)
                {
                    ++AttachCount;
                }
                else if (value == AttachmentLocation.Inline)
                {
                    ++InlineCount;
                }
                else
                {
                    throw new Exception("Invalid location.");
                }
                _location = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        //		public SmtpAttachment():base(){}

        /// <summary>
        /// Constructor for passing all information at once.
        /// </summary>
        /// <param name="fileName">File to send.</param>
        /// <param name="contentType">Content type of file (application/octet-stream for regular attachments.)</param>
        /// <param name="location">Where to put the attachment.</param>
        public MimeAttachment(string fileName, ContentType contentType, AttachmentLocation location)
            : base(fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(nameof(fileName));
            }
            FileName = fileName;
            ContentType = contentType;
            Location = location;
        }


        /// <summary>
        /// Shortcut constructor for passing regular style attachments.
        /// </summary>
        /// <param name="filename">File to send.</param>
        public MimeAttachment(string filename)
            : this(filename, new ContentType(MediaTypeNames.Application.Octet), AttachmentLocation.Attachmed)
        {
        }

        /// <summary>
        /// Constructor for passing stream attachments.
        /// </summary>
        /// <param name="contentStream">Stream to the attachment contents</param>
        /// <param name="name">Name of the attachment as it will appear on the e-mail</param>
        /// <param name="contentStream">Where to put the attachment</param>
        public MimeAttachment(Stream contentStream, string name, AttachmentLocation location = AttachmentLocation.Attachmed)
            :this(contentStream, name, new ContentType(MediaTypeNames.Application.Octet), location)
        {
        }

        /// <summary>
        /// Constructor for passing stream attachments.
        /// </summary>
        /// <param name="contentStream">Stream to the attachment contents</param>
        /// <param name="name">Name of the attachment as it will appear on the e-mail</param>
        /// <param name="contentType">Content type of the attachment</param>
        /// <param name="contentStream">Where to put the attachment</param>
        public MimeAttachment(Stream contentStream, string name, ContentType contentType, AttachmentLocation location = AttachmentLocation.Attachmed)
            : base(contentStream, name)
        {
            Location = location;
            ContentType = contentType;
        }

        /// <summary>
        /// Show this attachment.
        /// </summary>
        /// <returns>The file name of the attachment.</returns>
        public override string ToString()
        {
            return FileName;
        }
    
    }

#endregion
    /// <summary>
    /// For use in SmtpAttachment.
    /// </summary>
    public enum AttachmentLocation
    {
        /// <summary>
        /// Send as attachment.
        /// </summary>
        Attachmed = 0,
        /// <summary>
        /// Send as MIME inline (for html images, etc).
        /// </summary>
        Inline = 1
    }


}
