using System.Net.Mail;
using System.Text;

namespace AegisImplicitMail
{
    public class MimeMailAddress : MailAddress, IMailAddress
    {
        public MimeMailAddress(string address) : base(address)
        {
        }

        public MimeMailAddress(string address, string displayName) : base(address, displayName)
        {
        }

        public MimeMailAddress(string address, string displayName, Encoding displayNameEncoding) : base(address, displayName, displayNameEncoding)
        {
        }
    }
}
