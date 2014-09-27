/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Mail (AIM)
 * Aegis Implicit Mail is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * Aegis Implicit Mail is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System;
using System.ComponentModel;
using System.Windows.Forms;
using AegisImplicitMail;

namespace TestSslMail
{
    public partial class MainForm : Form
    {
        private const string Host = "smtp.gmail.com";
        private const string User = "you@gmail.com";
        private const string Pass = "password";
        private const string Mail = "you@gmail.com";
        private const int TlsPort = 587;


        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MimeMessage().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SmimeMessage().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mymessage = new MimeMailMessage
            {
                From = new MimeMailAddress(Mail),
                Subject = "testImplicit",
                Body = "body"
            };
            mymessage.To.Add(Mail);
            var mailer = new MimeMailer(Host, 465)
            {
                User = User,
                Password = Pass,
                EnableImplicitSsl = true,
                AuthenticationMode = AuthenticationType.Base64
            };
            ((SmtpSocketClient) mailer).SslType = SslMode.Ssl;

            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);
        }

        private void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                Console.Out.WriteLine(e.UserState.ToString());
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!e.Cancelled)
            {
                MessageBox.Show("Send successfull!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var mailer = new MimeMailer(Host, TlsPort, User, Pass)
            {
                Timeout = 200000,
                SslType = SslMode.Auto,
                AuthenticationMode = AuthenticationType.Base64
            };
            mailer.SendCompleted += compEvent;
            mailer.TestConnection();
            if (mailer.SupportsTls)
            {
                MessageBox.Show("Server Supports TLS");
            }
            else
            {
                MessageBox.Show("TLS is not Supported");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var mymessage = new MimeMailMessage
            {
                From = new MimeMailAddress(Mail),
                Body = "body",
                Subject = "test explicit"
            };
            mymessage.To.Add(Mail);
            var mailer = new MimeMailer(Host, TlsPort)
            {
                User = User,
                Password = Pass,
                SslType = SslMode.Tls,
                AuthenticationMode = AuthenticationType.Base64
            };
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var mailer = new MimeMailer(Host, TlsPort)
            {
                User = User,
                Password = Pass,
                AuthenticationMode = AuthenticationType.Base64
            };
            mailer.SendCompleted += compEvent;
            MessageBox.Show("Ssl Type:" + mailer.DetectSslMode());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var mailer = new MimeMailer(Host, TlsPort)
            {
                User = User,
                Password = Pass,
                AuthenticationMode = AuthenticationType.Base64
            };

            //Uncomment following line to make it work
            //The connection settings are wrong because the SslType is not specified
            //mailer.SslType = SslMode.Tls;
            mailer.SendCompleted += compEvent;
            MessageBox.Show("Are connection settings correct?" + mailer.TestConnection());
        }
    }
}