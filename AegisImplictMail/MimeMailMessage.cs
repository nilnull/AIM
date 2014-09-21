/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implict Ssl Mailer (AISM)
 * Aegis Implict Ssl Mailer is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * Aegis Implict Ssl Mailer is an implict ssl package to use mine/smime messages on implict ssl servers
 */
using System;

namespace SslImplicitMail
{
    public class MimeMailMessage : AbstractMailMessage
    {
        private MimeAttachmentCollection _attachments;
        private bool _disposed = false;


        /// <summary>
        /// List of files to attach. 
        /// Note : Please do not use Attachment
        /// </summary>
        public new MimeAttachmentCollection Attachments
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(this.GetType().FullName);
                }

                if (_attachments == null)
                {
                   
                    _attachments = new MimeAttachmentCollection();
                }
                return _attachments;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                _disposed = true;
                base.Dispose(disposing);
             
                if (_attachments != null)
                {
                    _attachments.Dispose();
                }
            }

        }
    }
    
}