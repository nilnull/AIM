/*
 * SmtpEmailer : A class/library for sending SMTP messages.
 * Provided by Steaven Woyan swoyan@hotmail.com
 * This package is provided without any warranty.
 * By using this package you agree that the provider makes no guarantee of use
 * and shall not be held liable or accountable for use of this software, even if it
 * fails to work properly.
 * 
 * Parts of this package include logic/code design from PJ Naughter's C++ SMTP package
 * located at http://www.naughter.com/smpt.html.  This link is provided for documentation
 * purposes and is not meant to imply his use/acknowledgement/approval of this package.
 * 
 * This package is provided with source and is free for use for any reason except that
 * it may not be sold or re-packaged by itself.
 * 
 */
using System;

namespace smw.smtp
{
	/// <summary>
	/// For use in SmtpAttachment.
	/// </summary>
	public enum AttachmentLocation
	{
		/// <summary>
		/// Send as regular attachment.
		/// </summary>
		Attachment = 0,
		/// <summary>
		/// Send as MIME inline (for html images, etc).
		/// </summary>
		Inline = 1
	}

  /// <summary>
  /// Login type for authentication.
  /// </summary>
  public enum AuthenticationType
  {
    /// <summary>
    /// No authentication is used.
    /// </summary>
    None = -1,

    /// <summary>
    /// Base64 authentication is used.
    /// </summary>
    Base64 = 0,

    /// <summary>
    /// Plain text authentication is used.
    /// </summary>
    Plain = 1
  }
	/// <summary>
	/// Class for holding an attachment's information.
	/// </summary>
	public class SmtpAttachment
	{
		internal static int inlineCount;
		internal static int attachCount;
		internal AttachmentLocation location;
		/// <summary>
		/// File to send.
		/// </summary>
		public string FileName;
		/// <summary>
		/// Content type of file (application/octet-stream for regular attachments).
		/// </summary>
		public string ContentType;
		
		/// <summary>
		/// Where to put the attachment.
		/// </summary>
		public AttachmentLocation Location
		{
			get {return location;}
			set
			{				
				if(value == AttachmentLocation.Attachment)
				{
					++attachCount;
				}
				else if(value == AttachmentLocation.Inline)
				{
					++inlineCount;
				}
				else
				{
					throw new Exception("Invalid location.");
				}
				location = value;
			}
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SmtpAttachment(){}

		/// <summary>
		/// Constructor for passing all information at once.
		/// </summary>
		/// <param name="fileName">File to send.</param>
		/// <param name="contentType">Content type of file (application/octet-stream for regular attachments.)</param>
		/// <param name="location">Where to put the attachment.</param>
		public SmtpAttachment(string fileName, string contentType, AttachmentLocation location)
		{
			this.FileName = fileName;
			this.ContentType = contentType;
			this.Location = location;
		}

		/// <summary>
		/// Shortcut constructor for passing regular style attachments.
		/// </summary>
		/// <param name="filename">File to send.</param>
		public SmtpAttachment(string filename) : this(filename, "application/octet-stream", AttachmentLocation.Attachment)
		{			
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
