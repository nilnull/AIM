using System;
using System.Collections.ObjectModel;

namespace net.scan.aegis.ace.SecureMail
{
    /// <summary>
    /// Represents a collection of mail attachments.
    /// </summary>
    public class SecureAttachmentCollection : Collection<SecureAttachment>
    {
        # region Constructors

        /// <summary>
        /// Creates a new instance of the SecureAttachmentCollection class.
        /// </summary>
        internal SecureAttachmentCollection()
        {
        }

        # endregion

        # region Methods

        /// <summary>
        /// Called when a user adds an item to the collection.
        /// </summary>
        /// <param name="index">The index where the item will be inserted.</param>
        /// <param name="item">The item to be inserted.</param>
        protected override void InsertItem(int index, SecureAttachment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Called when a user replaces an existing item in the collection.
        /// </summary>
        /// <param name="index">The index of the item to be replaced.</param>
        /// <param name="item">The new value of the item.</param>
        protected override void SetItem(int index, SecureAttachment item)
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
