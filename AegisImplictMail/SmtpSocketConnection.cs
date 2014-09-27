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
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AegisImplicitMail
{	
	/// <summary>
	/// Class for handling an SMTP connection.
	/// </summary>
	internal class SmtpSocketConnection :IDisposable
	{
        /// <summary>
        /// Get the client side certificate for ssl validation
        /// </summary>
	    public X509Certificate2 ClientCertificate2 { get; set; }
        public X509CertificateCollection clientcerts { get; set; }
        //variables
		private TcpClient _socket;
		private StreamReader _reader;
		private StreamWriter _writer;
		private bool _connected;

		/// <summary>
		/// Connection status.
		/// </summary>
		public bool Connected
		{
			get{return _connected;}
		}

		/// <summary>
		/// Create a new connection.
		/// </summary>
		internal SmtpSocketConnection()
		{
			_socket = new TcpClient();
		}

	    readonly RemoteCertificateValidationCallback _validationCallback =
  ServerValidationCallback;

        private static bool ServerValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
        {
            Console.Out.WriteLine("Validation Callback " + certificate + " ||| " + sslpolicyerrors);
            return true;
        }

     //   LocalCertificateSelectionCallback selectionCallback ;

        private X509Certificate ClientCertificateSelectionCallback(object sender, string targethost, X509CertificateCollection localcertificates, X509Certificate remotecertificate, string[] acceptableissuers)
        {
            return ClientCertificate2;
        }

	    private const EncryptionPolicy EncryptionPolicy = System.Net.Security.EncryptionPolicy.AllowNoEncryption;
	    private SslProtocols _sslProtocol = SslProtocols.Default;
	
	    private string _host;

	    public SslProtocols SslProtocol
	    {
            get { return _sslProtocol; }
            set { _sslProtocol = value; }
	    }

	    /// <summary>
	    /// Open connection to host on port.
	    /// </summary>
	    /// <param name="host">Host name to connect to.</param>
	    /// <param name="port">Port to connect to.</param>
	    /// <param name="isSsl">Enable SSL if it's an ssl</param>
	    /// <param name="timeout">Determins the time that you allow your application to do transaction, default is 100000</param>
	    /// <exception cref="ArgumentException"></exception>
	    internal void Open(string host, int port = 465, SslMode isSsl = SslMode.None, int timeout = 100000)
		{
            if (string.IsNullOrWhiteSpace(host) || port <= 0)
            {
                throw new ArgumentException("Invalid Argument found.");
            }
	        _host = host;
            _socket.Connect(host, port);
	        _socket.SendTimeout = timeout;
	        _socket.ReceiveTimeout = timeout;
	        if (isSsl == SslMode.Ssl)
	        {

	            if (clientcerts == null)
	            {

	                var sslStream = new SslStream(_socket.GetStream(),
	                    true, _validationCallback, ClientCertificateSelectionCallback, EncryptionPolicy);
	                sslStream.AuthenticateAsClient(host);
	                _writer = new StreamWriter(sslStream, Encoding.ASCII);
	                _reader = new StreamReader(sslStream, Encoding.ASCII);
	            }
	            else
	            {
	              var  sslStream = new SslStream(_socket.GetStream(),
	                    true, _validationCallback, ClientCertificateSelectionCallback, EncryptionPolicy);
	                sslStream.AuthenticateAsClient(host, clientcerts, _sslProtocol, CheckRevokation);
                    _writer = new StreamWriter(sslStream, Encoding.ASCII);
                    _reader = new StreamReader(sslStream, Encoding.ASCII);
                    
	            }
	            _connected = true;

	        }
            else
            {
                _reader = new StreamReader(_socket.GetStream(), Encoding.ASCII);
                _writer = new StreamWriter(_socket.GetStream(), Encoding.ASCII);
                _connected = true;
	
            }
                    
		}

	    public bool CheckRevokation { get; set; }

	    /// <summary>
		/// Close connection.
		/// </summary>
		internal void Close()
		{
          _reader.Close();
	      _writer.Flush();
			_writer.Close();
			_reader = null;
			_writer = null;
			_socket.Close();
			_connected = false;
		}

		/// <summary>
		/// Send a command to the server.
		/// </summary>
		/// <param name="cmd">Command to send.</param>
		internal void SendCommand(string cmd)
		{
			_writer.WriteLine(cmd);
			_writer.Flush();
		}


		/// <summary>
		/// Send data to the server. Used for sending attachments.
		/// </summary>
		/// <param name="buf">Data buffer.</param>
		/// <param name="start">Starting position in buffer.</param>
		/// <param name="length">Length to send.</param>
		internal void SendData(char[] buf, int start, int length)
		{
			_writer.Write(buf, start, length);
		}



		/// <summary>
		/// Get the reply message from the server.
		/// </summary>
		/// <param name="reply">Text reply from server.</param>
		/// <param name="code">Status code from server.</param>
		internal void GetReply(out string reply, out int code)
		{
            GetReply(_reader,out reply,out code);
		}


	    /// <summary>
	    /// Get the reply message from the server.
	    /// </summary>
	    /// <param name="reader">the reader which we are going to read</param>
	    /// <param name="reply">Text reply from server.</param>
	    /// <param name="code">Status code from server.</param>
	    private void GetReply(StreamReader reader, out string reply, out int code)
        {
            try
            {
                var s = reader.ReadLine();
                reply = s;
                while (s != null && s.Substring(3, 1).Equals("-"))
                {
                    s = reader.ReadLine();
                    if (s != null)
                    {
                        reply += s + "\r\n";
                    }
                }
                code = reply == null ? -1 : Int32.Parse(reply.Substring(0, 3));
            }
            catch (Exception err)
            {
                reply = "Error in reading response from server. " + err.Message;
                code = -1;
            }
        }


	    public void Dispose()
	    {
            _socket.Close();
            _socket = null;
	    }

	    internal void SwitchToSsl()
	    {
            if (clientcerts == null)
            {

                var sslStream = new SslStream(_socket.GetStream(),
                    true, _validationCallback, ClientCertificateSelectionCallback, EncryptionPolicy);
                sslStream.AuthenticateAsClient(_host);
                _writer = new StreamWriter(sslStream, Encoding.ASCII);
                _reader = new StreamReader(sslStream, Encoding.ASCII);
            }
            else
            {
               var sslStream = new SslStream(_socket.GetStream(),
                    true, _validationCallback, ClientCertificateSelectionCallback, EncryptionPolicy);
                sslStream.AuthenticateAsClient(_host, clientcerts, _sslProtocol, CheckRevokation);
                _writer = new StreamWriter(sslStream, Encoding.ASCII);
                _reader = new StreamReader(sslStream, Encoding.ASCII);

            }
	   
	    }
	}
}
