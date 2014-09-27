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

using System.Net.Mail;

namespace AegisImplicitMail
{
    /// <summary>
    /// Abstract class for mail messages, all sending mail classes should accept this class
    /// If you want to generate your own custom message object please extend this class
    /// </summary>
    public abstract class AbstractMailMessage : MailMessage
    {
        protected AbstractMailMessage(string from, string to, string subject, string body)
            : base(from, to, subject, body)
        {
        }

        protected AbstractMailMessage(string from, string to)
            : base(from, to)
        {
        }

        protected AbstractMailMessage()
        {
        }


 
    }


}
