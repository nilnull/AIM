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
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestSslMail.Dialogs
{
    public partial class GetUserWithCert : Form
    {
        public X509Certificate2 PrivateKey { get; private set; }
        public X509Certificate2 PublicKey { get; private set; }
        public string DisplayName { get; set; }
        public string MailAddress { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Regular expression, which is used to validate an E-Mail address.
        /// </summary>
        public const string MatchEmailPattern =
                  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        /// <summary>
        /// Checks whether the given Email-Parameter is a valid E-Mail address.
        /// </summary>
        /// <param name="email">Parameter-string that contains an E-Mail address.</param>
        /// <returns>True, when Parameter-string is not null and 
        /// contains a valid E-Mail address;
        /// otherwise false.</returns>
        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            return false;
        }

        public GetUserWithCert(string title = "Please choos a user")
        {
            InitializeComponent();
            groupBox1.Text = title;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            bool error = false;
            DisplayName = textBox1.Text;
            if (IsEmail(textBox2.Text))
            {
                try
                {
                    PrivateKey = new X509Certificate2(textBox3.Text, textBox4.Text, X509KeyStorageFlags.Exportable);
                    if (!PrivateKey.HasPrivateKey)
                    {
                        MessageBox.Show("Your file does not contained any private key");
                        PrivateKey = null;
                        error = true;
                    }
                }
                catch (Exception errException)
                {
                    if (
                        MessageBox.Show("An error happened " + errException.Message + "\n Do you want to retry?",
                            "Error!",
                            MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    {
                        okBtn_Click(sender, e);
                    }
                    else
                    {
                        error = true;
                    }

                }
                if (!error)
                {
                    try
                    {
                        PublicKey = new X509Certificate2(textBox5.Text);
                    }
                    catch (Exception err)
                    {
                        PublicKey = MessageBox.Show(
                            "We couldn't open you r public key" + err.Message +
                            "\n Do you want to use your private key file as the public key as well?", "Warning",
                            MessageBoxButtons.YesNo) == DialogResult.Yes ? PrivateKey : null;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
            }
        }
    }
}
