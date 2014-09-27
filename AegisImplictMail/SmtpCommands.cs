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

namespace AegisImplicitMail
{

        internal static class SmtpCommands
        {
            internal const string Auth = ("AUTH ");
            internal const string AuthPlian = "PLAIN";
            internal const string AuthLogin = "LOGIN";

            internal const string Data = ("DATA");
            internal const string Date = ("DATE: ");

            internal const string EHello = ("EHLO ");
            internal const string From = ("FROM: ");
            internal const string To = ("TO: ");
            internal const string Cc = ("CC: ");
            internal const string Bcc = ("BCC: ");
            internal const string ReplyTo = ("REPLY-TO: ");
            internal const string Subject = ("SUBJECT: ");

            internal const string Recipient = ("RCPT TO:");
            

            internal const string Hello = ("HELO ");
            internal const string Mail = ("MAIL FROM:");
            internal const string Utf8 = ("SMTPUTF8");
            internal const string Quit = ("QUIT");
            
            
            internal const string Noop = ("NOOP\r\n");
            internal const string Help = ("HELP");

            internal const string Reset = ("RSET\r\n");
            internal const string Send = ("SEND FROM:");
            internal const string SendAndMail = ("SAML FROM:");
            internal const string SendOrMail = ("SOML FROM:");
            internal const string Turn = ("TURN\r\n");
            internal const string Verify = ("VRFY ");
            internal const string StartTls = ("STARTTLS");

            static SmtpCommands()
            {
            }
        }
    }

