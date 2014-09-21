using System;
using System.Text;
using System.Text.RegularExpressions;
using net.scan.ace.SecureMail;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Provides methods to perform various encoding tasks.
    /// </summary>
    public static class TransferEncoder
    {
        # region Constants

        private const int MAX_CHARS_PER_LINE = 76;

        private static readonly string[] quotedPrintableChars = {
            "=00", "=01", "=02", "=03", "=04", "=05", "=06", "=07", "=08", "\t", "=0A", "=0B", 
            "=0C", "=0D", "=0E", "=0F", "=10", "=11", "=12", "=13", "=14", "=15", "=16", "=17", 
            "=18", "=19", "=1A", "=1B", "=1C", "=1D", "=1E", "=1F", " ", "!", "\"", "#", "$", 
            "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", 
            "5", "6", "7", "8", "9", ":", ";", "<", "=3D", ">", "?", "@", "A", "B", "C", "D", 
            "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
            "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", 
            "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", 
            "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "=7F", "=80", "=81", "=82", 
            "=83", "=84", "=85", "=86", "=87", "=88", "=89", "=8A", "=8B", "=8C", "=8D", "=8E", 
            "=8F", "=90", "=91", "=92", "=93", "=94", "=95", "=96", "=97", "=98","=99", "=9A", 
            "=9B", "=9C", "=9D", "=9E", "=9F", "=A0", "=A1", "=A2", "=A3", "=A4", "=A5", "=A6", 
            "=A7", "=A8", "=A9", "=AA", "=AB", "=AC", "=AD", "=AE", "=AF", "=B0", "=B1", "=B2", 
            "=B3", "=B4", "=B5", "=B6", "=B7", "=B8", "=B9", "=BA", "=BB", "=BC", "=BD", "=BE", 
            "=BF", "=C0", "=C1", "=C2", "=C3", "=C4", "=C5", "=C6", "=C7", "=C8", "=C9", "=CA", 
            "=CB", "=CC", "=CD", "=CE", "=CF", "=D0", "=D1", "=D2", "=D3", "=D4", "=D5", "=D6", 
            "=D7", "=D8", "=D9", "=DA", "=DB", "=DC", "=DD", "=DE", "=DF", "=E0", "=E1", "=E2", 
            "=E3", "=E4", "=E5", "=E6", "=E7", "=E8", "=E9", "=EA", "=EB", "=EC", "=ED", "=EE", 
            "=EF", "=F0", "=F1", "=F2", "=F3", "=F4", "=F5", "=F6", "=F7", "=F8", "=F9", "=FA", 
            "=FB", "=FC", "=FD", "=FE", "=FF" };

        # endregion

        # region Methods

        internal static string GetTransferEncodingName(SecureTransferEncoding encoding)
        {
            switch (encoding)
            {
                case SecureTransferEncoding.SevenBit:
                    return "7bit";
                case SecureTransferEncoding.QuotedPrintable:
                    return "quoted-printable";
                case SecureTransferEncoding.Base64:
                    return "base64";
                default:
                    throw new ArgumentOutOfRangeException("encoding");
            }
        }

        internal static System.Net.Mime.TransferEncoding ConvertTransferEncoding(SecureTransferEncoding encoding)
        {
            switch (encoding)
            {
                case SecureTransferEncoding.SevenBit:
                    return System.Net.Mime.TransferEncoding.SevenBit;
                case SecureTransferEncoding.QuotedPrintable:
                    return System.Net.Mime.TransferEncoding.QuotedPrintable;
                case SecureTransferEncoding.Base64:
                    return System.Net.Mime.TransferEncoding.Base64;
                default:
                    throw new ArgumentOutOfRangeException("encoding");
            }
        }

        /// <summary>
        /// Replaces all single instances of CR or LF with a CRLF pair.
        /// </summary>
        /// <param name="s">A string to normalize.</param>
        /// <returns>The normalized string.</returns>
        public static string NormalizeLinefeeds(string s)
        {
            // Replace any CRs which have no LF with a CRLF pair.
            s = Regex.Replace(s, @"\r(?!\n)", "\r\n");
            // Replace any LFs which have no CR with a CRLF pair.
            s = Regex.Replace(s, @"(?<!\r)\n", "\r\n");

            return s;
        }

        /// <summary>
        /// Converts a message to Base64.
        /// </summary>
        /// <param name="bytes">A byte array to convert.</param>
        /// <returns>A string containing the converted byte array.</returns>
        public static string ToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);
        }

        /// <summary>
        /// Converts a message to quoted-printable.
        /// </summary>
        /// <param name="bytes">A byte array to convert.</param>
        /// <param name="encodeNewlines">True to encode CRLF pairs to =0D=0A; False to include CRLF without encoding them.</param>
        /// <returns>A string containing the conveted byte array.</returns>
        public static string ToQuotedPrintable(byte[] bytes, bool encodeNewlines)
        {
            StringBuilder returnValue = new StringBuilder();

            int currentColumn = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (IsNewline(bytes, i) && !encodeNewlines)
                {
                    if (i > 0 && IsWhitespace(bytes[i - 1]))
                    {
                        returnValue.Length -= 1;

                        if (bytes[i - 1] == ' ')
                        {
                            returnValue.Append("=20");
                        }
                        else if (bytes[i - 1] == '\t')
                        {
                            returnValue.Append("=09");
                        }
                    }

                    i++;

                    returnValue.Append("\r\n");
                    currentColumn = 0;
                }
                else if (currentColumn >= MAX_CHARS_PER_LINE - 4)
                {
                    returnValue.Append("=\r\n");
                    currentColumn = 0;
                    i--;
                }
                else if (IsWhitespace(bytes[i]))
                {
                    returnValue.Append(quotedPrintableChars[bytes[i]]);
                    currentColumn += quotedPrintableChars[bytes[i]].Length;
                }
                else // non-whitespace and non-linefeed
                {
                    int bytesTillWhitespace;

                    int chars = CharactersUntilNextWhitespace(bytes, i, encodeNewlines, out bytesTillWhitespace);

                    if (chars > MAX_CHARS_PER_LINE - 4)
                    {
                        if (currentColumn != 0)
                        {
                            returnValue.Append("=\r\n");
                            currentColumn = 0;
                        }

                        for (int offset = 0; offset < bytesTillWhitespace; offset++)
                        {
                            returnValue.Append(quotedPrintableChars[bytes[i + offset]]);
                            currentColumn += quotedPrintableChars[bytes[i + offset]].Length;

                            if (currentColumn >= MAX_CHARS_PER_LINE - 4)
                            {
                                returnValue.Append("=\r\n");
                                currentColumn = 0;
                            }
                        }

                        if (bytesTillWhitespace > 0)
                        {
                            i += bytesTillWhitespace - 1;
                        }

                    }
                    else if (chars + currentColumn >= MAX_CHARS_PER_LINE - 4)
                    {
                        returnValue.Append("=\r\n");
                        currentColumn = 0;

                        for (int offset = 0; offset < bytesTillWhitespace; offset++)
                        {
                            returnValue.Append(quotedPrintableChars[bytes[i + offset]]);
                            currentColumn += quotedPrintableChars[bytes[i + offset]].Length;
                        }

                        if (bytesTillWhitespace > 0)
                        {
                            i += bytesTillWhitespace - 1;
                        }
                    }
                    else
                    {
                        for (int offset = 0; offset < bytesTillWhitespace; offset++)
                        {
                            returnValue.Append(quotedPrintableChars[bytes[i + offset]]);
                            currentColumn += quotedPrintableChars[bytes[i + offset]].Length;
                        }

                        if (bytesTillWhitespace > 0)
                        {
                            i += bytesTillWhitespace - 1;
                        }

                    }
                }
            }

            if (returnValue.Length > 0)
            {
                switch (returnValue[returnValue.Length - 1])
                {
                    case ' ':
                        returnValue.Length--;
                        returnValue.Append("=20");
                        break;
                    case '\t':
                        returnValue.Length--;
                        returnValue.Append("=09");
                        break;
                }
            }


            return returnValue.ToString();
        }

        private static bool IsNewline(byte[] bytes, int currentPosition)
        {
            return currentPosition < bytes.Length - 1
               && bytes[currentPosition] == '\r'
               && bytes[currentPosition + 1] == '\n';
        }

        private static bool IsWhitespace(byte character)
        {
            return (character == '\t' || character == ' ');
        }

        private static int CharactersUntilNextWhitespace(
            byte[] bytes, int currentPosition, bool encodeNewlines, out int bytesRead)
        {
            int returnValue = 0;
            bytesRead = 0;

            while (currentPosition < bytes.Length &&
                !IsWhitespace(bytes[currentPosition])
                && (encodeNewlines || !IsNewline(bytes, currentPosition))
                && returnValue <= MAX_CHARS_PER_LINE)
            {
                bytesRead++;
                returnValue += quotedPrintableChars[bytes[currentPosition]].Length;

                currentPosition++;
            }

            return returnValue;
        }

        # endregion
    }
}
