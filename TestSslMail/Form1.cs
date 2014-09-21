/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Ssl Mailer (AISM)
 * Aegis Implicit Ssl Mailer is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
 * Aegis Implicit Ssl Mailer is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;
using SslImplicitMail;
using TestEmailer;

namespace TestSslMail
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (to.Text.Trim().Length > 0)
            {
                toList.Add(new MimeMailAddress(to.Text));
            }
        }

        public List<IMailAddress> toList { get; set; }
        public List<IMailAddress> ccList { get; set; }
        public List<IMailAddress> bccList { get; set; }
        public List<Attachment> attachList { get; set; }

        private void button2_Click(object sender, System.EventArgs e)
        {
            SmtpSocketClient emailer = new SmtpSocketClient();
            emailer.Host = host.Text;
            emailer.Port = Convert.ToInt16(port.Text);
            emailer.MailMessage.From = new MailAddress("farhang@scan-associates.net","Araz Farhang");
            emailer.MailMessage.Subject = subject.Text;
            emailer.MailMessage.Body = body.Text;
            emailer.MailMessage.IsBodyHtml = checkHTML.Checked;
            for (int x = 0; x < toList.Count; ++x)
            {
                emailer.MailMessage.To.Add((MimeMailAddress)toList[x]); 
            }
            for (int x = 0; x < ccList.Count; ++x)
            {
                emailer.MailMessage.CC.Add((MimeMailAddress)ccList[x]);
            }
            for (int x = 0; x < bccList.Count; ++x)
            {
                emailer.MailMessage.Bcc.Add((MimeMailAddress)bccList[x]);
            }
            for (int x = 0; x < attachList.Count; ++x)
            {
                emailer.MailMessage.Attachments.Add((MimeAttachment)attachList[x]);
            }
            if (!loginNone.Checked)
            {
                emailer.User = "farhang";
                emailer.Password = password.Text;
                if (loginBase64.Checked)
                {
                    emailer.AuthenticationMode = AuthenticationType.Base64;
                }
                else
                {
                    emailer.AuthenticationMode = AuthenticationType.PlainText;
                }
            }
            emailer.OnMailSent += new SendCompletedEventHandler(OnMailSent);
            emailer.SendMessageAsync(emailer.MailMessage);
            statusBar.Text = "Sending...";


        }

        private void OnMailSent(object sender, AsyncCompletedEventArgs asynccompletedeventargs)
        {

            Console.Out.WriteLine(asynccompletedeventargs.UserState.ToString());
        }

        private void OnMailSent(string msg)
        {
            Console.Out.WriteLine("result: "+msg);
         //   statusBar.Text = "Mail sent.";
        //    MessageBox.Show(this, "The mail has been sent.", "Mail Sent", MessageBoxButtons.OK,
          //      MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MimeAttachment a = new MimeAttachment(dlg.FileName);
                AttachType at = new AttachType();
                at.ShowDialog(this);
                if (at.DialogResult == DialogResult.OK)
                {
                    a.ContentType =  new ContentType(at.contentType.Text);
                    a.Location = at.attachAttachment.Checked ? AttachmentLocation.Attachmed : AttachmentLocation.Inline;
                    attachList.Add(a);
                }
            }
        }

        private void loginBase64_CheckedChanged(object sender, System.EventArgs e)
        {
            password.Enabled = true;
        }

        private void loginPlain_CheckedChanged(object sender, System.EventArgs e)
        {
            password.Enabled = true;
        }

        private void loginNone_CheckedChanged(object sender, System.EventArgs e)
        {
            password.Enabled = false;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            if (to.Text.Trim().Length > 0)
            {
                ccList.Add(new MimeMailAddress(to.Text)); 
            }
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            if (to.Text.Trim().Length > 0)
            {
                bccList.Add( new MimeMailAddress(to.Text));
            }
        }

        class human 
        {
            public int ghad { get; set; }
            public void NegahKon(human aHuman)
            {
                MessageBox.Show(aHuman.ghad.ToString());
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            human ehsan  =new human();
            ehsan.ghad = 120;
            human asghar = new human();
            asghar.ghad = 90;
            ehsan.NegahKon(asghar);
            MessageBox.Show(ehsan.ghad.ToString());
        }
    }


}

