using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

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
        /// Attachment is a File Stream
        /// </summary>
        public bool isFileStream { get; private set; } = false;

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

        public string GetEncodedAttachmentName()
        {
            Encoding displayNameEncoding = this.NameEncoding ?? Encoding.UTF8;
            if (displayNameEncoding.Equals(Encoding.ASCII))
            {
                return this.ContentType.Name;
            }
            else
            {
                string encodingName = displayNameEncoding.BodyName.ToLower();
                string encodedName = $"=?{encodingName}?B?{Convert.ToBase64String(displayNameEncoding.GetBytes(this.ContentType.Name))}?=";
                return encodedName;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AegisImplicitMail.MimeAttachment class with the specified 
        /// content string.
        /// </summary>
        /// <param name="fileName">A System.String that contains a file path to use to create this attachment</param>
        /// <param name="location">Attached or Inline</param>
        public MimeAttachment(string fileName, AttachmentLocation location = AttachmentLocation.Attachmed)
            : base(fileName)
        {
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the AegisImplicitMail.MimeAttachment class with the specified
        /// content string and System.Net.Mime.ContentType.
        /// </summary>
        /// <param name="fileName">A System.String that contains a file path to use to create this attachment.</param>
        /// <param name="contentType">A System.Net.Mime.ContentType that describes the data in string.</param>
        /// <param name="location">Attached or Inline</param>
        public MimeAttachment(string fileName, ContentType contentType, AttachmentLocation location = AttachmentLocation.Attachmed)
            : base(fileName, contentType)
        {
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the AegisImplicitMail.MimeAttachment class with the specified
        /// stream and name.
        /// </summary>
        /// <param name="contentStream">A readable System.IO.Stream that contains the content for this attachment.</param>
        /// <param name="name">
        /// A System.String that contains the value for the System.Net.Mime.ContentType.Name
        /// property of the System.Net.Mime.ContentType associated with this attachment.
        /// To ensure compatibility with the most number of email providers, this value cannot be null.
        /// </param>
        /// <param name="location">Attached or Inline</param>
        public MimeAttachment(Stream contentStream, string name, AttachmentLocation location = AttachmentLocation.Attachmed) 
            : base(contentStream, name)
        {
            isFileStream = true;
            Location = location;
        }

        /// <summary>
        /// Initializes a new instance of the AegisImplicitMail.MimeAttachment class with the specified
        /// stream and content type.
        /// </summary>
        /// <param name="contentStream"> A readable System.IO.Stream that contains the content for this attachment.</param>
        /// <param name="contentType">A System.Net.Mime.ContentType that describes the data in stream.</param>
        /// <param name="location">Attached or Inline</param>
        public MimeAttachment(Stream contentStream, ContentType contentType, AttachmentLocation location = AttachmentLocation.Attachmed)
            :base(contentStream, contentType) 
        {
            isFileStream = true;
            Location = location;
        }

        /// <summary>
        /// Constructor for passing stream attachments.
        /// This constructor has no equivalent in System.Net.Mail.Attachment constructors.
        /// Stays here for compatibility reasons.
        /// </summary>
        /// <param name="contentStream">Stream to the attachment contents</param>
        /// <param name="name">Name of the attachment as it will appear on the e-mail</param>
        /// <param name="contentType">Content type of the attachment</param>
        /// <param name="location">Attached or Inline</param>
        public MimeAttachment(Stream contentStream, string name, ContentType contentType, AttachmentLocation location = AttachmentLocation.Attachmed)
            : this(contentStream, name)
        {
            isFileStream = true;
            Location = location;
            ContentType.Name = name;
        }

        /// <summary>
        /// Show this attachment.
        /// </summary>
        /// <returns>The name of the attachment.</returns>
        public override string ToString()
        {
            return this.ContentType.Name;
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
