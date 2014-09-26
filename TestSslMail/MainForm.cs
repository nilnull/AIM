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

        string host = "smtp.gmail.com";
        string user = "yourmail@gmail.com";
        string pass = "password#";
        string mail = "yourmail@gmail.com";
        private int tlsPort = 587;


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
            var mymessage = new MimeMailMessage();
            mymessage.From = new MimeMailAddress(mail);
            mymessage.To.Add(mail);
            mymessage.Subject = "test";
            mymessage.Body = "body";
            var mailer = new MimeMailer(host, 465);
               mailer.User= user;
            mailer.Password = pass;
            ((SmtpSocketClient) mailer).SslType = SslMode.Ssl;
            mailer.EnableImplicitSsl = true;
            mailer.AuthenticationMode = AuthenticationType.Base64;
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);
        }

        private void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState!=null)
                Console.Out.WriteLine(e.UserState.ToString());
            Console.Out.WriteLine("is it canceled? " + e.Cancelled);

            if (e.Error != null)
                Console.Out.WriteLine("Error : " + e.Error.Message);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            var mailer = new MimeMailer(host, 465, user, pass);
            mailer.Timeout = 200;
            mailer.SendCompleted += compEvent;
            mailer.TestConnection();

            Console.Out.WriteLine(mailer.SupportsTls );
       
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var mymessage = new MimeMailMessage
            {
                From = new MimeMailAddress(mail),
             Body  = "body",
             Subject = "test explicit"
            };
            mymessage.To.Add(mail);
            var mailer = new MimeMailer(host, tlsPort)
            {
                User = user,
                Password = pass,
                SslType = SslMode.Tls,
                EnableImplicitSsl = false,
                AuthenticationMode = AuthenticationType.Base64
            };
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mymessage);

       
        }

        }
    }

