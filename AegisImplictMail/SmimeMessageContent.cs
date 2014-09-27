using System;
using System.Net.Mime;
using System.Text;

namespace AegisImplicitMail
{
    internal class SmimeMessageContent
    {
        # region Constructors

        public SmimeMessageContent(byte[] body, ContentType contentType, TransferEncoding transferEncoding, bool encodeBody)
        {
            if (encodeBody)
            {
                switch (transferEncoding)
                {
                    case TransferEncoding.QuotedPrintable:
                        Body = Encoding.ASCII.GetBytes(TransferEncoder.ToQuotedPrintable(body, false));
                        break;
                    case TransferEncoding.Base64:
                        Body = Encoding.ASCII.GetBytes(TransferEncoder.ToBase64(body));
                        break;
                    case TransferEncoding.SevenBit:
                        Body = body;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("transferEncoding", "Invalid Transfer Encoding");
                }
            }
            else
            {
                Body = body;
            }

            TransferEncoding = transferEncoding;
            ContentType = contentType;
        }

        # endregion

        # region Properties

        public ContentType ContentType
        {
            get;
            private set;
        }

        public TransferEncoding TransferEncoding
        {
            get;
            private set;
        }

        public byte[] Body
        {
            get;
            private set;
        }

        # endregion
    }
}
