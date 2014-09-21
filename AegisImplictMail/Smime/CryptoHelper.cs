using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SslImplictMail.Smime
{
    public static class CryptoHelper
    {
        internal static byte[] GetSignature(string message, X509Certificate2 signingCertificate, X509Certificate2 encryptionCertificate)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            SignedCms signedCms = new SignedCms(new ContentInfo(messageBytes), true);

            CmsSigner cmsSigner = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, signingCertificate);
            cmsSigner.IncludeOption = X509IncludeOption.WholeChain;

            if (encryptionCertificate != null)
            {
                cmsSigner.Certificates.Add(encryptionCertificate);
            }

            var signingTime = new Pkcs9SigningTime();
            cmsSigner.SignedAttributes.Add(signingTime);
            
            signedCms.ComputeSignature(cmsSigner, false); 

            return signedCms.Encode();
        }

        /// <summary>
        /// Encrypts a message
        /// </summary>
        /// <param name="message">The message to encrypt</param>
        /// <param name="encryptionCertificates">A list of certificates to encrypt the message with</param>
        /// <returns>The encrypted message</returns>
        internal static byte[] EncryptMessage(string message, X509Certificate2Collection encryptionCertificates)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            EnvelopedCms envelopedCms = new EnvelopedCms(new ContentInfo(messageBytes));

            CmsRecipientCollection recipients = new CmsRecipientCollection(SubjectIdentifierType.IssuerAndSerialNumber, encryptionCertificates);

            envelopedCms.Encrypt(recipients); 

            return envelopedCms.Encode();
        }

    }
}
