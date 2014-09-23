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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TestSslMail.Dialogs
{
    public partial class GetUser : Form
    {
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

        public GetUser(string title = "Enter User Info")
        {
            InitializeComponent();
            
            Title = title;
            groupBox1.Text = title;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
         

            DisplayName = textBox1.Text;
            if (IsEmail(textBox2.Text))
            {
                MailAddress = textBox2.Text;
            }
            else
            {
                MailAddress = null;
                MessageBox.Show("You have entered an invalid mail address");
               
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!IsEmail(textBox2.Text))
            {
                MessageBox.Show("Please enter a valid email address");
                textBox2.Text = "";
            }
             
        }
    }
}
