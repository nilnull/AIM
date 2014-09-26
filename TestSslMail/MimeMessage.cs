/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Mail (AIM)
 * Aegis Implicit Mail is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
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
    public partial class MimeMessage : Form
    {


        public MimeMessage()
        {
            InitializeComponent();
            var ssltypes = new SslMode();
            comboBox1.DataSource = Enum.GetValues(ssltypes.GetType());
            comboBox1.SelectedIndex = 0;

            ResetAll();
            
        }

        private void ResetAll()
        {
            ToList = new List<IMailAddress>();
            CcList = new List<IMailAddress>();
            BccList = new List<IMailAddress>();
            _senderMail = null;
            host.Text = "smtp.gmail.com";
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
            var userGetter = new GetUser("Please choose Reciever");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    ToList.Add(new MimeMailAddress(userGetter.MailAddress, userGetter.DisplayName));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    ToList.Add( new MimeMailAddress(userGetter.MailAddress));
                }
                to.Text += txt;
            }


        }

        private List<IMailAddress> ToList { get; set; }
        private List<IMailAddress> CcList { get; set; }
        private List<IMailAddress> BccList { get; set; }
        private List<Attachment> AttachList { get; set; }
        private MimeMailAddress _senderMail;
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
            
            var mailMessage = new MimeMailMessage();
            mailMessage.Subject = subjectText;
            mailMessage.Body = bodyText;
            mailMessage.Sender = _senderMail;
            mailMessage.IsBodyHtml = sendAsHtml;
            mailMessage.From = _senderMail;
            var emailer = new MimeMailer(hostAddress,portNo)
            {
                Host = hostAddress,
                Port = portNo,
                MailMessage = mailMessage,
                SslType = (SslMode)comboBox1.SelectedItem
            };

            for (int x = 0; x < ToList.Count; ++x)
            {
                emailer.MailMessage.To.Add((MimeMailAddress) ToList[x]);
            }
            for (int x = 0; x < CcList.Count; ++x)
            {
                emailer.MailMessage.CC.Add((MimeMailAddress) CcList[x]);
            }
            for (int x = 0; x < BccList.Count; ++x)
            {
                emailer.MailMessage.Bcc.Add((MimeMailAddress) BccList[x]);
            }
            for (int x = 0; x < AttachList.Count; ++x)
            {
                emailer.MailMessage.Attachments.Add((MimeAttachment) AttachList[x]);
            }
            if (!loginNone.Checked)
            {
                emailer.User = userName.Text;
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
            else
            {
                emailer.AuthenticationMode = AuthenticationType.UseDefualtCridentials;
            }
            emailer.SendCompleted += SendCompleted;
            emailer.SendMailAsync(emailer.MailMessage);
        }

        private void SendCompleted(object sender, AsyncCompletedEventArgs asynccompletedeventargs)
        {
            if (!String.IsNullOrEmpty(asynccompletedeventargs.UserState.ToString()))
            MessageBox.Show(asynccompletedeventargs.UserState.ToString());
            Console.Out.WriteLine("is it canceled? " + asynccompletedeventargs.Cancelled);

            if (asynccompletedeventargs.Error !=null)
            Console.Out.WriteLine("Error : " +asynccompletedeventargs.Error.Message);
        }

        private void button3_Click(object sender, EventArgs e)
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
                    AttachList.Add(a);
                }
            }
        }

        private void loginBase64_CheckedChanged(object sender, EventArgs e)
        {
            userName.Enabled = password.Enabled = true;
        }

        private void loginPlain_CheckedChanged(object sender,EventArgs e)
        {
            userName.Enabled = password.Enabled = true;
        }

        private void loginNone_CheckedChanged(object sender, EventArgs e)
        {
            password.Enabled = false;
            userName.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            var txt = "";
            var userGetter = new GetUser("Please choose CC");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    CcList.Add(new MimeMailAddress(userGetter.MailAddress, userGetter.DisplayName));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    CcList.Add( new MimeMailAddress(userGetter.MailAddress));
                }
                cc.Text += txt;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var txt = "";
            var userGetter = new GetUser("Please choose BCC");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    BccList.Add(new MimeMailAddress(userGetter.MailAddress, userGetter.DisplayName));
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    BccList.Add( new MimeMailAddress(userGetter.MailAddress));
                }
                bcc.Text += txt;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var txt = "";
            var userGetter = new GetUser("Please choose sender");
            if (userGetter.ShowDialog() == DialogResult.OK)
            {

                if (!String.IsNullOrWhiteSpace(userGetter.DisplayName) &&
                    !String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = userGetter.DisplayName + " " + "<" + userGetter.MailAddress + "> ;";

                    _senderMail = new MimeMailAddress(userGetter.MailAddress, userGetter.DisplayName);
                }
                else if (!String.IsNullOrWhiteSpace(userGetter.MailAddress))
                {
                    txt = "<" + userGetter.MailAddress + "> ;";

                    _senderMail = new MimeMailAddress(userGetter.MailAddress);
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

    }


}

