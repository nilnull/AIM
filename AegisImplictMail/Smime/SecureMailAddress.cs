using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Represents the address of an electronic mail sender or recipient.
    /// </summary>
    public class SecureMailAddress : MailAddress
    {
        # region Constructors

        /// <summary>
        /// Initializes a new instance of the SecureMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        public SecureMailAddress(string address) :base(address)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        public SecureMailAddress(string address, string displayName):base(address,displayName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        /// <param name="encryptionCert">An encryption certificate.  (Private key not required.)</param>
        public SecureMailAddress(string address, string displayName, X509Certificate2 encryptionCert)
            : this(address, displayName)
        {
            EncryptionCertificate = encryptionCert;
        }

        /// <summary>
        /// Initializes a new instance of the SecureMailAddress class.
        /// </summary>
        /// <param name="address">An email address.</param>
        /// <param name="displayName">A display name.</param>
        /// <param name="encryptionCert">An encryption certificate.  (Private key not required.)</param>
        /// <param name="signingCert">A signing certificate. (Private key required.)</param>
        public SecureMailAddress(string address, string displayName, X509Certificate2 encryptionCert, X509Certificate2 signingCert)
            : this(address, displayName, encryptionCert)
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

        

        /// <summary>
        /// Gets the internal System.Net.Mail.MailAddress which this object wraps.
        /// </summary>
//        internal System.Net.Mail.MailAddress InternalMailAddress
 //       {
  //          get;
   //         private set;
    //    }

        # endregion
    }
}
