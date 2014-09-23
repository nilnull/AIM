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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AegisImplicitMail
{
    /// <summary>
    ///The Mail adress of recipient together with it's certificates in-order to do cryptography functions.
    /// The diffrence between this type of Mail Address with original mail address is having signing and encryption certificates
    /// </summary>
    public class SmimeMailAddress : MailAddress,IMailAddress
    {
        # region Constructors

        /// <summary>
        /// Initializes a new instance of the SmimeMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        public SmimeMailAddress(string address) :base(address)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SmimeMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        public SmimeMailAddress(string address, string displayName):base(address,displayName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SmimeMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        /// <param name="encryptionCert">An encryption certificate.  (Private key not required.)</param>
        public SmimeMailAddress(string address, string displayName, X509Certificate2 encryptionCert)
            : this(address, displayName)
        {
            EncryptionCertificate = encryptionCert;
        }

        /// <summary>
        /// Initializes a new instance of the SmimeMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        /// <param name="encryptionCert">An encryption certificate.  (Private key not required.)</param>
        /// <param name="signingCert">A signing certificate. (Private key required.)</param>
        public SmimeMailAddress(string address, string displayName, X509Certificate2 encryptionCert, X509Certificate2 signingCert)
            : this(address, displayName, encryptionCert)
        {
            if (signingCert != null && !signingCert.HasPrivateKey)
            {
                throw new CryptographicException("The specified signing certificate doesn't contain a private key.");
            }

            SigningCertificate = signingCert;
        }

        public SmimeMailAddress(string address, X509Certificate2 encryptionCert, X509Certificate2 signingCert)
            : this(address, null, encryptionCert,signingCert)
        {
            if (signingCert != null && !signingCert.HasPrivateKey)
            {
                throw new CryptographicException("The specified signing certificate doesn't contain a private key.");
            }

            SigningCertificate = signingCert;
        }
        # endregion

        # region Properties

        /// <summary>
        /// Gets the e-mail address specified when this instance was created.
        /// </summary>
        
        
        /// <summary>
        /// Gets the signing certificate specified when this instance was created.
        /// </summary>
        public X509Certificate2 SigningCertificate
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the encryption certificate specified when this instance was created.
        /// </summary>
        public X509Certificate2 EncryptionCertificate
        {
            get;
            private set;
        }
        # endregion
    }
}
