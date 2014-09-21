/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implict Ssl Mailer (AIM)
 * Aegis Implict Ssl Mailer is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * Aegis Implict Ssl Mailer is an implict ssl package to use mine/smime messages on implict ssl servers
 */
using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace AegisImplicitMail
{

    public class SmtpSocketClient : IDisposable
    {
        const string authExtension = "AUTH";
        const string authLogin = "LOGIN";
        const string authPlian = "PLAIN";
        private const string gap = " ";
        const string authGssapi = "gssapi";
        const string authWDigest = "wdigest";

        #region variables
		/// <summary>
		/// Delegate for mail sent notification.
		/// </summary>


		/// <summary>
		/// The delegate function which is called after mail has been sent.
		/// </summary>
		public event SendCompletedEventHandler OnMailSent;
        private SmtpSocketConnection _con;
        private int _port;
        private bool _sendAsHtml;
        private AuthenticationType _authMode = AuthenticationType.UseDefualtCridentials;
        private string _user;
        private string _password;
        private MimeMailMessage _mailMessage;


        private string _host;
        /// <summary>
        /// Name of server.
        /// </summary>
        public string Host
        {
            get { return _host; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Host shouldn't be empty or null. Invalid host name.");
                }
                _host = value;
            }
        }

        /// <summary>
		/// Port number of server server.
		/// </summary>
		public int Port
		{
			get {return _port;}
			set 
			{
				if(value <= 0)
				{
					throw new ArgumentException("Invalid port.");
				}
				_port = value;
			}
		}

      
    /// <summary>
    /// Method used for authentication.
    /// </summary>
    public AuthenticationType AuthenticationMode
    {
      get {return _authMode;}
      set {_authMode = value;}
    }

    /// <summary>
    /// User ID for authentication.
    /// </summary>
    public string User
    {
      get {return _user;}
      set {_user = value;}
    }

    /// <summary>
    /// Password for authentication.
    /// </summary>
    public string Password
    {
      get {return _password;}
      set {_password = value;}
    }

        public MimeMailMessage MailMessage
        {
            get { return _mailMessage; }
            set { _mailMessage = value; }
        }

        public bool EnableSsl { get; set; }

        #endregion

        #region cunstructor

        /// <summary>
        /// Generate a smtp socket client object
        /// </summary>
        /// <param name="host">Host address</param>
        /// <param name="port">Port Number</param>
        /// <param name="username">User name to login into server</param>
        /// <param name="password">Password</param>
        /// <param name="authenticationMode">Mode of authentication</param>
        /// <param name="useHtml">Determine if mail message is html or not</param>
        /// <param name="msg">Message to send</param>
        /// <param name="onMailSend">This function will be called after mail is sent</param>
        /// <param name="enableSsl">Your connection is Ssl conection?</param>
        /// <exception cref="ArgumentNullException">If username and pass is needed and not provided</exception>
        public SmtpSocketClient(string host, int port, string username =null, string password = null, AuthenticationType authenticationMode = AuthenticationType.Base64, bool useHtml =true, MimeMailMessage msg =null , SendCompletedEventHandler onMailSend =null, bool enableSsl = true ):this(msg)
        {
            if ((AuthenticationMode != AuthenticationType.UseDefualtCridentials) &&
                (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)))
            {
                throw new ArgumentNullException("username");
            }
            
            _host = host;
            _port = port;
            _user = username;
            _password = password;
            _authMode = authenticationMode;
            _mailMessage = msg;
            _sendAsHtml = useHtml;
            OnMailSent = onMailSend;
            EnableSsl = enableSsl;

        }

        public SmtpSocketClient(MimeMailMessage msg =null, bool enableSsl =true)
        {
		    if (msg == null)
		    {
		        msg = new MimeMailMessage();
		    }
            _mailMessage = msg;
            EnableSsl = enableSsl;
        }

#endregion

        #region MessageSenders
        /// <summary>
		/// Send the message.
		/// </summary>
		public void SendMessage(AbstractMailMessage message)
        {
            MailMessage = (MimeMailMessage) message;
			lock(this)
			{
				//do some sanity checking
				if(string.IsNullOrWhiteSpace(_host))
				{
					throw new ArgumentException("There wasn't any host address found for the mail.");
				}
				if(String.IsNullOrEmpty(MailMessage.From.Address))
				{
					throw new Exception("There wasn't any sender for the message");
				}
				if(MailMessage.To.Count == 0)
				{
					throw new Exception("Please specifie at least one reciever for the message");
				}
				//set up initial connection
				_con = new SmtpSocketConnection();
				if(_port <= 0) _port = 465;
				_con.Open(_host, _port,EnableSsl);
				var buf = new StringBuilder();
				string response;
				int code;
				//read greeting
				_con.GetReply(out response, out code);
				//introduce ourselves
        if(_authMode == AuthenticationType.UseDefualtCridentials)
        {
          buf.Append("HELO ");
          buf.Append(_host);
          _con.SendCommand(buf.ToString());
          _con.GetReply(out response, out code);
        }
        else
        {
          buf.Append("EHLO ");
          buf.Append(_host);
          _con.SendCommand(buf.ToString());
          _con.GetReply(out response, out code);          
          switch(_authMode)
          {
            case AuthenticationType.Base64:
              _con.SendCommand(authExtension+gap + authLogin);
              _con.GetReply(out response, out code);              
              _con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(_user)));
              _con.GetReply(out response, out code);
              _con.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(_password)));
              _con.GetReply(out response, out code);
              break;

            case AuthenticationType.PlainText:
              _con.SendCommand( authExtension+ gap+authLogin + gap+ authPlian);
              _con.GetReply(out response, out code);          
              _con.SendCommand(_user);
              _con.GetReply(out response, out code);
              _con.SendCommand(_password);
              _con.GetReply(out response, out code);
              break;
          }
        }
        buf.Length = 0;
				buf.Append("MAIL FROM:<");
				buf.Append(MailMessage.From);
				buf.Append(">");
				_con.SendCommand(buf.ToString());
				_con.GetReply(out response, out code);
				buf.Length = 0;			
				//set up list of to addresses
				foreach(MailAddress o in MailMessage.To)
				{
					buf.Append("RCPT TO:<");
					buf.Append(o);
					buf.Append(">");
					_con.SendCommand(buf.ToString());
					_con.GetReply(out response, out code);
					buf.Length = 0;
				}
        //set up list of cc addresses
        buf.Length = 0;			
        foreach(MailAddress o in MailMessage.CC)
        {
          buf.Append("RCPT TO:<");
          buf.Append(o);
          buf.Append(">");
          _con.SendCommand(buf.ToString());
          _con.GetReply(out response, out code);
          buf.Length = 0;
        }
        //set up list of bcc addresses
        buf.Length = 0;			
        foreach(MailAddress o in MailMessage.Bcc)
        {          
          buf.Append("RCPT TO:<");
          buf.Append(o);
          buf.Append(">");
          _con.SendCommand(buf.ToString());
          _con.GetReply(out response, out code);
          buf.Length = 0;
        }
        buf.Length = 0;			
        //set headers
				_con.SendCommand("DATA");
				_con.SendCommand("X-Mailer: SslMail.SmtpEmailer");
				DateTime today = DateTime.Now;			
				buf.Append("DATE: ");
				buf.Append(today.ToLongDateString());
				_con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("FROM: ");
				buf.Append(MailMessage.From);
				_con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("TO: ");
				buf.Append(MailMessage.To[0]);
				for(int x = 1; x < MailMessage.To.Count; ++x)
				{
					buf.Append(";");
					buf.Append(MailMessage.To[x]);				
				}
				_con.SendCommand(buf.ToString());
        if(MailMessage.CC.Count > 0)
        {
          buf.Length = 0;
          buf.Append("CC: ");
          buf.Append(MailMessage.CC[0]);
          for(int x = 1; x < MailMessage.CC.Count; ++x)
          {
            buf.Append(";");
            buf.Append(MailMessage.CC[x]);				
          }
          _con.SendCommand(buf.ToString());
        }
        if(MailMessage.Bcc.Count > 0)
        {
          buf.Length = 0;
          buf.Append("BCC: ");
          buf.Append(MailMessage.Bcc[0]);
          for (int x = 1; x < MailMessage.Bcc.Count; ++x)
          {
            buf.Append(";");
            buf.Append(MailMessage.Bcc[x]);				
          }
          _con.SendCommand(buf.ToString());
        }
        buf.Length = 0;
				buf.Append("REPLY-TO: ");
				buf.Append(MailMessage.From);
				_con.SendCommand(buf.ToString());
				buf.Length = 0;			
				buf.Append("SUBJECT: ");
				buf.Append(MailMessage.Subject);
				_con.SendCommand(buf.ToString());				
				buf.Length = 0;
				//declare mime info for message
				_con.SendCommand("MIME-Version: 1.0");
				if(!_sendAsHtml || (_sendAsHtml && ((MimeAttachment.InlineCount > 0) || (MimeAttachment.AttachCount > 0))))
				{
					_con.SendCommand("Content-Type: multipart/mixed; boundary=\"#SEPERATOR1#\"\r\n");				
					_con.SendCommand("This is a multi-part message.\r\n\r\n--#SEPERATOR1#");
				}
				if(_sendAsHtml)
				{
					_con.SendCommand("Content-Type: multipart/related; boundary=\"#SEPERATOR2#\"");				
					_con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					_con.SendCommand("--#SEPERATOR2#");
					
				}
				if(_sendAsHtml && MimeAttachment.InlineCount > 0)
				{
					_con.SendCommand("Content-Type: multipart/alternative; boundary=\"#SEPERATOR3#\"");				
					_con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					_con.SendCommand("--#SEPERATOR3#");
					_con.SendCommand("Content-Type: text/html; charset=iso-8859-1");				
					_con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					_con.SendCommand(EncodeBodyAsQuotedPrintable());
					_con.SendCommand("--#SEPERATOR3#");
					_con.SendCommand("Content-Type: text/plain; charset=iso-8859-1");									
					_con.SendCommand("\r\nIf you can see this, then your email client does not support MHTML messages.");
					_con.SendCommand("--#SEPERATOR3#--\r\n");
					_con.SendCommand("--#SEPERATOR2#\r\n");
					SendAttachments(buf, AttachmentLocation.Inline);					
				}
				else
				{
					if(_sendAsHtml)
					{
						_con.SendCommand("Content-Type: text/html; charset=iso-8859-1");				
						_con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					}
					else
					{
						_con.SendCommand("Content-Type: text/plain; charset=iso-8859-1");				
						_con.SendCommand("Content-Transfer-Encoding: quoted-printable\r\n");		
					}
					_con.SendCommand(EncodeBodyAsQuotedPrintable());
				}
				if(_sendAsHtml)
				{
					_con.SendCommand("\r\n--#SEPERATOR2#--");
				}
				if(MimeAttachment.AttachCount > 0)
				{
					//send normal attachments
					SendAttachments(buf, AttachmentLocation.Attachmed);
				}
				//finish up message
				_con.SendCommand("");
				if(MimeAttachment.InlineCount > 0 || MimeAttachment.AttachCount > 0)
				{
					_con.SendCommand("--#SEPERATOR1#--");
				}
				_con.SendCommand(".");
				_con.GetReply(out response, out code);
        Console.WriteLine(response);
				_con.SendCommand("QUIT");
				_con.GetReply(out response, out code);
        Console.WriteLine(response);
				_con.Close();
				_con = null;
                
				if(OnMailSent != null)
				{
					OnMailSent(this, new AsyncCompletedEventArgs(null,false,response));
				}
			}
		}

		/// <summary>
		/// Send the message on a seperate thread.
		/// </summary>
		public void SendMessageAsync(AbstractMailMessage message = null)
		{
		    if (message == null)
		        message = this.MailMessage;
			new Thread(()=> SendMessage(message)).Start();
		}

		/// <summary>
		/// Send any attachments.
		/// </summary>
		/// <param name="buf">String work area.</param>
		/// <param name="type">Attachment type to send.</param>
		private void SendAttachments(StringBuilder buf, AttachmentLocation type)
		{
            
			//declare mime info for attachment
			var fbuf = new byte[2048];
		    string seperator = type == AttachmentLocation.Attachmed ? "\r\n--#SEPERATOR1#" : "\r\n--#SEPERATOR2#";
			buf.Length = 0;
			foreach(MimeAttachment o in MailMessage.Attachments)
			{									
				MimeAttachment attachment = o;
				if(attachment.Location != type)
				{
					continue;
				}																			
				var cs = new CryptoStream(new FileStream(attachment.FileName, FileMode.Open, FileAccess.Read, FileShare.Read), new ToBase64Transform(), CryptoStreamMode.Read);
				_con.SendCommand(seperator);
				buf.Append("Content-Type: ");
				buf.Append(attachment.ContentType);
				buf.Append("; name=");
				buf.Append(Path.GetFileName(attachment.FileName));
				_con.SendCommand(buf.ToString());
				_con.SendCommand("Content-Transfer-Encoding: base64");
				buf.Length = 0;
				buf.Append("Content-Disposition: attachment; filename=");
				buf.Append(Path.GetFileName(attachment.FileName));
				_con.SendCommand(buf.ToString());
				buf.Length = 0;
				buf.Append("Content-ID: ");
				buf.Append(Path.GetFileNameWithoutExtension(attachment.FileName));				
				buf.Append("\r\n");
				_con.SendCommand(buf.ToString());								
				buf.Length = 0;
				int num = cs.Read(fbuf, 0, 2048);
				while(num > 0)
				{					
					_con.SendData(Encoding.ASCII.GetChars(fbuf, 0, num), 0, num);
					num = cs.Read(fbuf, 0, 2048);
				}
				cs.Close();
				_con.SendCommand("");
			}
		}

        #endregion
		
        /// <summary>
		/// Encode the body as in quoted-printable format.
		/// Adapted from PJ Naughter's quoted-printable encoding code.
		/// For more information see RFC 2045.
		/// </summary>
		/// <returns>The encoded body.</returns>
		private string EncodeBodyAsQuotedPrintable()
		{
			var stringBuilder = new StringBuilder();
			sbyte currentByte;
			foreach (char t in MailMessage.Body)
			{
			    currentByte = (sbyte) t;
			    //is this a valid ascii character?
			    if( ((currentByte >= 33) && (currentByte <= 60)) || ((currentByte >= 62) && (currentByte <= 126)) || (currentByte == '\r') || (currentByte == '\n') || (currentByte == '\t') || (currentByte == ' '))
			    {
			        stringBuilder.Append(t);
			    }
			    else
			    {
			        stringBuilder.Append('=');
			        stringBuilder.Append(((sbyte)((currentByte & 0xF0) >> 4)).ToString("X"));
			        stringBuilder.Append(((sbyte) (currentByte & 0x0F)).ToString("X"));
			    }
			}
			//format data so that lines don't end with spaces (if so, add a trailing '='), etc.
			//for more detail see RFC 2045.
			int start = 0;
			string encodedString = stringBuilder.ToString();
			stringBuilder.Length = 0;
			for(int x = 0; x < encodedString.Length; ++x)
			{
				currentByte = (sbyte) encodedString[x];
				if(currentByte == '\n' || currentByte == '\r' || x == (encodedString.Length - 1))
				{
					stringBuilder.Append(encodedString.Substring(start, x - start + 1));
					start = x + 1;
					continue;
				}
				if((x - start) > 76)
				{
					bool inWord = true;
					while(inWord)
					{
						inWord = (!char.IsWhiteSpace(encodedString, x) && encodedString[x-2] != '=');
						if(inWord)
						{
							--x;
//							currentByte = (sbyte) encodedString[x];
						}
						if(x == start)
						{
							x = start + 76;
							break;
						}
					}
					stringBuilder.Append(encodedString.Substring(start, x - start + 1));
					stringBuilder.Append("=\r\n");
					start = x + 1;
				}
			}
			return stringBuilder.ToString();
		}

        public void Dispose()   
        {
            if (_con.Connected)
            {
                _con.Close();
            }
            _mailMessage.Dispose();
        }
	}
}
