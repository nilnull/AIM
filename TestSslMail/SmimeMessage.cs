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
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;
using AegisImplicitMail;
using TestEmailer;
using TestSslMail.Dialogs;

namespace TestSslMail
{
    public partial class SmimeMessage : Form
    {

        private void ResetAll()
        {
            ToList = new List<IMailAddress>();
            CcList = new List<IMailAddress>();
            BccList = new List<IMailAddress>();
            _senderMail = null;
            to.Text = "";
            cc.Text = "";
            bcc.Text = "";
            senderText.Text = "";
            AttachList = new List<Attachment>();
            button2.Focus();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var txt = "";
            var userGetter = new GetUserWithCert();
            if (userGetter.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    ToList.Add(new SmimeMailAddress(userGetter.MailAddress, userGetter.DisplayName,userGetter.PublicKey,userGetter.PrivateKey));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    ToList.Add(new SmimeMailAddress(userGetter.MailAddress,userGetter.PublicKey,userGetter.PrivateKey));
                }
                to.Text += txt;
            }


        }

        private List<IMailAddress> ToList { get; set; }
        private List<IMailAddress> CcList { get; set; }
        private List<IMailAddress> BccList { get; set; }
        private List<Attachment> AttachList { get; set; }
        private SmimeMailAddress _senderMail;
        private void button2_Click(object sender, EventArgs e)
        {
            SendMail();
        }


        private void SendMail()
        {
            var hostAddress = host.Text;
            var portNo = Convert.ToInt16(port.Text);
            var subjectText = subject.Text;
            var bodyText = body.Text;
            var sendAsHtml = checkHTML.Checked;

            var mailMessage = new SmimeMailMessage
            {
                Subject = subjectText,
                Body = bodyText,
                Sender = _senderMail,
                IsBodyHtml = sendAsHtml,
                From = _senderMail
            };
            var emailer = new SmimeMailer(hostAddress, portNo)
            {
                MailMessage = mailMessage,
                SslType = SslMode.Tls
            };

            foreach (IMailAddress t in ToList)
            {
                emailer.MailMessage.To.Add((SmimeMailAddress)t);
            }
            foreach (IMailAddress t in CcList)
            {
                emailer.MailMessage.CC.Add((SmimeMailAddress)t);
            }
            foreach (IMailAddress t in BccList)
            {
                emailer.MailMessage.Bcc.Add((SmimeMailAddress)t);
            }
            foreach (Attachment t in AttachList)
            {
                emailer.MailMessage.Attachments.Add((MimeAttachment)t);
            }
            if (!loginNone.Checked)
            {
                emailer.User = userName.Text;
                emailer.Password = password.Text;
                emailer.AuthenticationMode = loginBase64.Checked ? AuthenticationType.Base64 : AuthenticationType.PlainText;
            }
            emailer.SendCompleted += SendCompleted;
            emailer.SendMailAsync(emailer.MailMessage);
        }

        private void SendCompleted(object sender, AsyncCompletedEventArgs asynccompletedeventargs)
        {

            Console.Out.WriteLine(asynccompletedeventargs.UserState.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {CheckFileExists = true, CheckPathExists = true};
            if (dlg.ShowDialog() != DialogResult.OK) return;
            var a = new MimeAttachment(dlg.FileName);
            var at = new AttachType();
            at.ShowDialog(this);
            if (at.DialogResult == DialogResult.OK)
            {
                a.ContentType = new ContentType(at.contentType.Text);
                a.Location = at.attachAttachment.Checked ? AttachmentLocation.Attachmed : AttachmentLocation.Inline;
                AttachList.Add(a);
            }
        }

        private void loginBase64_CheckedChanged(object sender, EventArgs e)
        {
            userName.Enabled = password.Enabled = true;
        }

        private void loginPlain_CheckedChanged(object sender, EventArgs e)
        {
            userName.Enabled = password.Enabled = true;
        }

        private void loginNone_CheckedChanged(object sender, EventArgs e)
        {
            password.Enabled = false;
            userName.Enabled = false;
        }

        private void cc_Click(object sender, EventArgs e)
        {

            var txt = "";
            var userGetter = new GetUserWithCert("Please choose CC");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    CcList.Add(new SmimeMailAddress(userGetter.MailAddress, userGetter.DisplayName,userGetter.PublicKey,userGetter.PrivateKey));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    CcList.Add( new SmimeMailAddress(userGetter.MailAddress,userGetter.PublicKey,userGetter.PrivateKey));
                }
                cc.Text += txt;
            }

        }

        private void bcc_Click(object sender, EventArgs e)
        {
            var txt = "";
            var userGetter = new GetUserWithCert("Please choose BCC");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    BccList.Add(new SmimeMailAddress(userGetter.MailAddress, userGetter.DisplayName,userGetter.PublicKey,userGetter.PrivateKey));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    BccList.Add(new SmimeMailAddress(userGetter.MailAddress,userGetter.PublicKey,userGetter.PrivateKey));
                }
                bcc.Text += txt;
            }
        }

        private void from_Click(object sender, EventArgs e)
        {
            var txt = "";
            var userGetter = new GetUserWithCert("Please choose sender");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    _senderMail = new SmimeMailAddress(userGetter.MailAddress, userGetter.DisplayName,userGetter.PublicKey,userGetter.PrivateKey);
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    _senderMail =  new SmimeMailAddress(userGetter.MailAddress,userGetter.PublicKey,userGetter.PrivateKey);
                }
                senderText.Text = txt;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        public SmimeMessage()
        {
            InitializeComponent();
        }

    }
}
