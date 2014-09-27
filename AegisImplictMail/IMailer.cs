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

using System.Collections.Generic;
using System.Net.Mail;

namespace AegisImplicitMail
{
    /// <summary>
    /// General interface to create mail sender. Any class which wants to generate and send mail should implement this mail address
    /// </summary>
    interface IMailer
    {
        /// <summary>
        /// Generate an email 
        /// </summary>
        /// <param name="sender">From Field of mail</param>
        /// <param name="toAddresses">Reciever address</param>
        /// <param name="ccAddresses">CC addresss</param>
        /// <param name="bccAddresses">Bcc Address</param>
        /// <param name="attachmentsList">List of attachments</param>
        /// <param name="subject">Message's subject</param>
        /// <param name="body">Body of message</param>
        /// <returns>Generatedc mail message</returns>
        AbstractMailMessage GenerateMail(IMailAddress sender, List<IMailAddress> toAddresses, List<IMailAddress> ccAddresses, List<IMailAddress> bccAddresses, List<string> attachmentsList, string subject, string body);
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <param name="onSendCallBack">Deligated function that should be called afte finishing the sending</param>
        void Send(AbstractMailMessage message, SendCompletedEventHandler onSendCallBack);
    }
}
