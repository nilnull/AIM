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
using System.Collections;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace smw.smtp
{
	/// <summary>
	/// A class for sending email messages with attachments via SMTP.
	/// Steaven Woyan swoyan@hotmail.com
	/// Version 1.4 February 23, 2003
	/// </summary>
	public class SmtpEmailer
	{				
		#region ItemList Helper Class
		/// <summary>
		/// A utilitiy class for holding the list of addresses.
		/// </summary>
		public class AddressList
		{
			ArrayList list;
			/// <summary>
			/// Create the AddressList.
			/// </summary>
			/// <param name="list">Associated array to store list data in.</param>
			internal AddressList(ArrayList list)
			{
				this.list = list;
			}
			/// <summary>
			/// Add an address to the list.
			/// </summary>
			/// <param name="address">Address to add.</param>
			public void Add(string address)
			{
        list.Add(address);
			}

			/// <summary>
			/// Remove an address from the list.
			/// </summary>
			/// <param name="address">Address to remove.</param>
			public void Remove(string address)
			{
				list.Remove(address);
			}
		}

		/// <summary>
		/// A utilitiy class for holding the list of to attachments.
		/// </summary>
		public class AttachmentList
		{
			ArrayList list;
			/// <summary>
			/// Create the AttachmentList.
			/// </summary>
			/// <param name="list">Associated array to store list data in.</param>
			internal AttachmentList(ArrayList list)
			{
				this.list = list;
			}
			/// <summary>
			/// Add an attachment.
			/// </summary>
			/// <param name="attachment">Attachment to add.</param>
			public void Add(SmtpAttachment attachment)
			{
				list.Add(attachment);
			}

			/// <summary>
			/// Remove an attachment from the list.
			/// </summary>
			/// <param name="attachment">Attachment to remove.</param>
			public void Remove(SmtpAttachment attachment)
			{
				list.Remove(attachment);
			}
		}

		#endregion

		/// <summary>
		/// Delegate for mail sent notification.
		/// </summary>
		public delegate void MailSentDelegate();

		/// <summary>
		/// Event sent when mail has been sent.
		/// </summary>
		public event MailSentDelegate OnMailSent;

		private SmtpConnection con;
		private string body;
		private string subject;
		internal ArrayList to;
    internal ArrayList cc;
    internal ArrayList bcc;
		internal ArrayList files;
		private string from;
		private string host;
		private int port;
		private AddressList to_receipients;
    private AddressList cc_receipients;
    private AddressList bcc_receipients;
		private AttachmentList attachments;
		private bool sendAsHtml;
    private AuthenticationType authMode = AuthenticationType.None;
    private string user;
    private string password;

		#region Properties
		/// <summary>
		/// Name of server.
		/// </summary>
		public string Host
		{
			get {return host;}
			set 
			{
				if(value == null || value.Trim().Length == 0)
				{
					throw new ArgumentException("Invalid host name.");
				}
				host = value;
			}
		}

		/// <summary>
		/// Port to connnect to on server.
		/// </summary>
		public int Port
		{
			get {return port;}
			set 
			{
				if(value <= 0)
				{
					throw new ArgumentException("Invalid port.");
				}
				port = value;
			}
		}	

		/// <summary>
		/// From email address.
		/// </summary>
		public string From
		{
			get {return from;}
			set
			{
				if(value == null || value.Trim().Length == 0)
				{
					throw new ArgumentException("Invalid from address.");
				}
				from = value;
			}
		}

		/// <summary>
		/// List of to email addresses.
		/// </summary>
		public SmtpEmailer.AddressList To
		{
			get {return to_receipients;}
		}

    /// <summary>
    /// List of cc email addresses.
    /// </summary>
    public SmtpEmailer.AddressList CC
    {
      get {return cc_receipients;}
    }

    /// <summary>
    /// List of bcc email addresses.
    /// </summary>
    public SmtpEmailer.AddressList BCC
    {
      get {return bcc_receipients;}
    }

    /// <summary>
		/// Subject of message.
		/// </summary>
		public string Subject
		{
			get {return subject;}
			set {subject = value;}
		}

		/// <summary>
		/// Body of message.
		/// </summary>
		public string Body
		{
			get {return body;}
			set {body = value;}
		}

		/// <summary>
		/// List of files to attach.
		/// </summary>
		public SmtpEmailer.AttachmentList Attachments
		{
			get {return attachments;}
		}

		/// <summary>
		/// Send body as HTML?
		/// </summary>
		public bool SendAsHtml
		{
			get {return sendAsHtml;}
			set {sendAsHtml = value;}
		}

    /// <summary>
    /// Method used for authentication.
    /// </summary>
    public AuthenticationType AuthenticationMode
    {
      get {return authMode;}
      set {authMode = value;}
    }

    /// <summary>
    /// User ID for authentication.
    /// </summary>
    public string User
    {
      get {return user;}
      set {user = value;}
    }

    /// <summary>
    /// Password for authentication.
    /// </summary>
    public string Password
    {
      get {return password;}
      set {password = value;}
    }

		#endregion

		/// <summary>
		/// Create an SmtpEmailer.
		/// </summary>
		public SmtpEmailer()		
		{						
			to = new ArrayList();
      cc = new ArrayList();
      bcc = new ArrayList();
			files = new ArrayList();
			to_receipients = new AddressList(to);
      cc_receipients = new AddressList(cc);
      bcc_receipients = new AddressList(bcc);
			attachments = new AttachmentList(files);
		}		

		/// <summary>
		/// Send the message.
		/// </summary>
		public void SendMessage()
		{
			lock(this)
			{
				//do some sanity checking
				if(host == null || host.Trim().Length == 0)
				{
					throw new Exception("No host specified.");
				}
				if(from == null || from.Trim().Length == 0)
				{
					throw new Exception("No from address.");
				}
				if(to.Count == 0)
				{
					throw new Exception("No to address.");
				}
				//set up initial connection
				con = new SmtpConnection();
				if(port <= 0) port = 25;
				con.Open(host, port);
				StringBuilder buf = new StringBuilder();
				string response;
				int code;
				//read greeting
				con.GetReply(out response, out code);
				//introduce ourselves
        if(authMode == AuthenticationType.None)
        {
          buf.Append("HELO ");
          buf.Append(host);
          con.SendCommand(buf.ToString());
          con.GetReply(out response, out code);
        }
        else
        {
          buf.Append("EHLO ");
          buf.Append(host);
          con.SendCommand(buf.ToString());
          con.GetReply(out response, out code);          
          switch(authMode)
          {
            case AuthenticationType.Base64:
              con.SendCommand("AUTH LOGIN");
              con.GetReply(out response, out code);              
              con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(user)));
              con.GetReply(out response, out code);
              con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)));
              con.GetReply(out response, out code);
              break;

            case AuthenticationType.Plain:
              con.SendCommand("AUTH LOGIN PLAIN");
              con.GetReply(out response, out code);          
              con.SendCommand(user);
              con.GetReply(out response, out code);
              con.SendCommand(password);
              con.GetReply(out response, out code);
              break;
          }
        }
        buf.Length = 0;
				buf.Append("MAIL FROM:<");
				buf.Append(from);
				buf.Append(">");
				con.SendCommand(buf.ToString());
				con.GetReply(out response, out code);
				buf.Length = 0;			
				//set up list of to addresses
				foreach(object o in to)
				{
					buf.Append("RCPT TO:<");
					buf.Append(o);
					buf.Append(">");
					con.SendCommand(buf.ToString());
					con.GetReply(out response, out code);
					buf.Length = 0;
				}
        //set up list of cc addresses
        buf.Length = 0;			
        foreach(object o in cc)
        {
          buf.Append("RCPT TO:<");
          buf.Append(o);
          buf.Append(">");
          con.SendCommand(buf.ToString());
          con.GetReply(out response, out code);
          buf.Length = 0;
        }
        //set up list of bcc addresses
        buf.Length = 0;			
        foreach(object o in bcc)
        {          
          buf.Append("RCPT TO:<");
          buf.Append(o);
          buf.Append(">");
          con.SendCommand(buf.ToString());
          con.GetReply(out response, out code);
          buf.Length = 0;
        }
        buf.Length = 0;			
        //set headers
				con.SendCommand("DATA");
				con.SendCommand("X-Mailer: smw.smtp.SmtpEmailer");
				DateTime today = DateTime.Now;			
				buf.Append("DATE: ");
				buf.Append(today.ToLongDateString());
				con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("FROM: ");
				buf.Append(from);
				con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("TO: ");
				buf.Append(to[0]);
				for(int x = 1; x < to.Count; ++x)
				{
					buf.Append(";");
					buf.Append(to[x]);				
				}
				con.SendCommand(buf.ToString());
        if(cc.Count > 0)
        {
          buf.Length = 0;
          buf.Append("CC: ");
          buf.Append(cc[0]);
          for(int x = 1; x < cc.Count; ++x)
          {
            buf.Append(";");
            buf.Append(cc[x]);				
          }
          con.SendCommand(buf.ToString());
        }
        if(bcc.Count > 0)
        {
          buf.Length = 0;
          buf.Append("BCC: ");
          buf.Append(bcc[0]);
          for(int x = 1; x < bcc.Count; ++x)
          {
            buf.Append(";");
            buf.Append(bcc[x]);				
          }
          con.SendCommand(buf.ToString());
        }
        buf.Length = 0;
				buf.Append("REPLY-TO: ");
				buf.Append(from);
				con.SendCommand(buf.ToString());
				buf.Length = 0;			
				buf.Append("SUBJECT: ");
				buf.Append(subject);
				con.SendCommand(buf.ToString());				
				buf.Length = 0;
				//declare mime info for message
				con.SendCommand("MIME-Version: 1.0");
				if(!sendAsHtml || (sendAsHtml && ((SmtpAttachment.inlineCount > 0) || (SmtpAttachment.attachCount > 0))))
				{
					con.SendCommand("Content-Type: multipart/mixed; boundary=\"#SEPERATOR1#\"\r\n");				
					con.SendCommand("This is a multi-part message.\r\n\r\n--#SEPERATOR1#");
				}
				if(sendAsHtml)
				{
					con.SendCommand("Content-Type: multipart/related; boundary=\"#SEPERATOR2#\"");				
					con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					con.SendCommand("--#SEPERATOR2#");
					
				}
				if(sendAsHtml && SmtpAttachment.inlineCount > 0)
				{
					con.SendCommand("Content-Type: multipart/alternative; boundary=\"#SEPERATOR3#\"");				
					con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					con.SendCommand("--#SEPERATOR3#");
					con.SendCommand("Content-Type: text/html; charset=iso-8859-1");				
					con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					con.SendCommand(EncodeBodyAsQuotedPrintable());
					con.SendCommand("--#SEPERATOR3#");
					con.SendCommand("Content-Type: text/plain; charset=iso-8859-1");									
					con.SendCommand("\r\nIf you can see this, then your email client does not support MHTML messages.");
					con.SendCommand("--#SEPERATOR3#--\r\n");
					con.SendCommand("--#SEPERATOR2#\r\n");
					SendAttachments(buf, AttachmentLocation.Inline);					
				}
				else
				{
					if(sendAsHtml)
					{
						con.SendCommand("Content-Type: text/html; charset=iso-8859-1");				
						con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					}
					else
					{
						con.SendCommand("Content-Type: text/plain; charset=iso-8859-1");				
						con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					}
					con.SendCommand(EncodeBodyAsQuotedPrintable());
				}
				if(sendAsHtml)
				{
					con.SendCommand("\r\n--#SEPERATOR2#--");
				}
				if(SmtpAttachment.attachCount > 0)
				{
					//send normal attachments
					SendAttachments(buf, AttachmentLocation.Attachment);
				}
				//finish up message
				con.SendCommand("");
				if(SmtpAttachment.inlineCount > 0 || SmtpAttachment.attachCount > 0)
				{
					con.SendCommand("--#SEPERATOR1#--");
				}
				con.SendCommand(".");
				con.GetReply(out response, out code);
        Console.WriteLine(response);
				con.SendCommand("QUIT");
				con.GetReply(out response, out code);
        Console.WriteLine(response);
				con.Close();
				con = null;
				if(OnMailSent != null)
				{
					OnMailSent();
				}
			}
		}

		/// <summary>
		/// Send the message on a seperate thread.
		/// </summary>
		public void SendMessageAsync()
		{
			new Thread(new ThreadStart(SendMessage)).Start();
		}

		/// <summary>
		/// Send any attachments.
		/// </summary>
		/// <param name="buf">String work area.</param>
		/// <param name="type">Attachment type to send.</param>
		private void SendAttachments(StringBuilder buf, AttachmentLocation type)
		{
			//declare mime info for attachment
			byte[] fbuf = new byte[2048];
			int num;
			SmtpAttachment attachment;
			string seperator = type == AttachmentLocation.Attachment ? "\r\n--#SEPERATOR1#" : "\r\n--#SEPERATOR2#";
			buf.Length = 0;
			foreach(object o in files)
			{									
				attachment = (SmtpAttachment) o;
				if(attachment.Location != type)
				{
					continue;
				}																			
				CryptoStream cs = new CryptoStream(new FileStream(attachment.FileName, FileMode.Open, FileAccess.Read, FileShare.Read), new ToBase64Transform(), CryptoStreamMode.Read);
				con.SendCommand(seperator);
				buf.Append("Content-Type: ");
				buf.Append(attachment.ContentType);
				buf.Append("; name=");
				buf.Append(Path.GetFileName(attachment.FileName));
				con.SendCommand(buf.ToString());
				con.SendCommand("Content-Transfer-Encoding: base64");
				buf.Length = 0;
				buf.Append("Content-Disposition: attachment; filename=");
				buf.Append(Path.GetFileName(attachment.FileName));
				con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("Content-ID: ");
				buf.Append(Path.GetFileNameWithoutExtension(attachment.FileName));				
				buf.Append("\r\n");
				con.SendCommand(buf.ToString());								
				buf.Length = 0;
				num = cs.Read(fbuf, 0, 2048);
				while(num > 0)
				{					
					con.SendData(Encoding.ASCII.GetChars(fbuf, 0, num), 0, num);
					num = cs.Read(fbuf, 0, 2048);
				}
				cs.Close();
				con.SendCommand("");
			}
		}

		/// <summary>
		/// Encode the body as in quoted-printable format.
		/// Adapted from PJ Naughter's quoted-printable encoding code.
		/// For more information see RFC 2045.
		/// </summary>
		/// <returns>The encoded body.</returns>
		private string EncodeBodyAsQuotedPrintable()
		{
			StringBuilder buf = new StringBuilder();
			sbyte cur;
			for(int x = 0; x < body.Length; ++x)
			{
				cur = (sbyte) body[x];
				//is this a valid ascii character?
				if( ((cur >= 33) && (cur <= 60)) || ((cur >= 62) && (cur <= 126)) || (cur == '\r') || (cur == '\n') || (cur == '\t') || (cur == ' '))
				{
					buf.Append(body[x]);
				}
				else
				{
					buf.Append('=');
					buf.Append(((sbyte)((cur & 0xF0) >> 4)).ToString("X"));
					buf.Append(((sbyte) (cur & 0x0F)).ToString("X"));
				}
			}
			//format data so that lines don't end with spaces (if so, add a trailing '='), etc.
			//for more detail see RFC 2045.

			int start = 0;
			string enc = buf.ToString();
			buf.Length = 0;
			for(int x = 0; x < enc.Length; ++x)
			{
				cur = (sbyte) enc[x];
				if(cur == '\n' || cur == '\r' || x == (enc.Length - 1))
				{
					buf.Append(enc.Substring(start, x - start + 1));
					start = x + 1;
					continue;
				}
				if((x - start) > 76)
				{
					bool inWord = true;
					while(inWord)
					{
						inWord = (!char.IsWhiteSpace(enc, x) && enc[x-2] != '=');
						if(inWord)
						{
							--x;
							cur = (sbyte) enc[x];
						}
						if(x == start)
						{
							x = start + 76;
							break;
						}
					}
					buf.Append(enc.Substring(start, x - start + 1));
					buf.Append("=\r\n");
					start = x + 1;
				}
			}
			return buf.ToString();
		}
	}
}
