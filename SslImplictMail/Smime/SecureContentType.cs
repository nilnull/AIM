using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace net.scan.ace.SecureMail
{
    /// <summary>
    /// Represents the content type of an email attachment.
    /// </summary>
    public class SecureContentType
    {
        # region Constructors

        /// <summary>
        /// Instantiates a new SecureContentType object.
        /// </summary>
        public SecureContentType()
        {
            InternalContentType = new System.Net.Mime.ContentType();
        }

        /// <summary>
        /// Instantiates a new SecureContentType object.
        /// </summary>
        /// <param name="contentType">The content type information in string format.</param>
        public SecureContentType(string contentType)
        {
            InternalContentType = new System.Net.Mime.ContentType(contentType);
        }

        # endregion

        # region Properties

        /// <summary>
        /// Gets or sets the name of the content.
        /// </summary>
        public string Name
        {
            get
            {
                return InternalContentType.Name;
            }
            set
            {
                InternalContentType.Name = value;
            }
        }

        /// <summary>
        /// Gets a list of parameter names and values associated with the content type.
        /// </summary>
        public StringDictionary Parameters
        {
            get
            {
                return InternalContentType.Parameters;
            }
        }

        /// <summary>
        /// Gets or sets the content's media type (eg: text/hml).
        /// </summary>
        public string MediaType
        {
            get
            {
                return InternalContentType.MediaType;
            }
            set
            {
                InternalContentType.MediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets the character set of the content.
        /// </summary>
        public string CharSet
        {
            get
            {
                return InternalContentType.CharSet;
            }
            set
            {
                InternalContentType.CharSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the boundary separator for multipart messages.
        /// </summary>
        internal string Boundary
        {
            get
            {
                return InternalContentType.Boundary;
            }
            set
            {
                InternalContentType.Boundary = value;
            }
        }

        /// <summary>
        /// Gets the internal representation of the content type which this class wraps.
        /// </summary>
        internal System.Net.Mime.ContentType InternalContentType
        {
            get;
            private set;
        }

        # endregion

        # region Methods

        /// <summary>
        /// Generates a random mime boundary.
        /// </summary>
        internal void GenerateBoundary()
        {
            Boundary = "--CPI=_" + Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns a string representation of the content type.
        /// </summary>
        /// <returns>A string representation of the content type.</returns>
        public override string ToString()
        {
            return InternalContentType.ToString();
        }

        # endregion
    }
}
