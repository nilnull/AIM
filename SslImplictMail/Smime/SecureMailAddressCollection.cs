using System;
using System.Collections.ObjectModel;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Stores e-mail addresses that are associated with an e-mail message.
    /// </summary>
    public class SecureMailAddressCollection
        : Collection<SecureMailAddress>
    {
        # region Constructors

        /// <summary>
        /// Initializes an empty instances of the SecureMailAddressCollection class.
        /// </summary>
        public SecureMailAddressCollection()
        {
        }

        # endregion

        # region Methods

        /// <summary>
        /// Adds a list of e-mail addresses to the collection.
        /// </summary>
        /// <param name="addresses">The addresses to add to the collection.  Multiple e-mail addressses must be separated with a comma.</param>
        public void Add(string addresses)
        {
            System.Net.Mail.MailAddressCollection parser = new System.Net.Mail.MailAddressCollection();
            parser.Add(addresses);

            foreach (System.Net.Mail.MailAddress address in parser)
            {
                this.Add(new SecureMailAddress(address.Address));
            }
        }

        /// <summary>
        /// Inserts an e-mail address into the SecuremailAddressCollection at the specified location.
        /// </summary>
        /// <param name="index">The location at which to insert the e-mail address.</param>
        /// <param name="item">The e-mail address to insert.</param>
        protected override void InsertItem(int index, SecureMailAddress item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the e-mail address element to be replaced.</param>
        /// <param name="item">An e-mail address which will replace the element in the collection.</param>
        protected override void SetItem(int index, SecureMailAddress item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.SetItem(index, item);
        }

        # endregion
    }
}
