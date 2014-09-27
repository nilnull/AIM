#region

using System;
using System.Collections.ObjectModel;

#endregion

namespace AegisImplicitMail
{
    /// <summary>
    ///     A collection of SmtpAttachments
    /// </summary>
    public class MimeAttachmentCollection : Collection<MimeAttachment>, IDisposable
    {
        private bool disposed;

        internal MimeAttachmentCollection()
        {
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            foreach (var attachment in this)
            {
                attachment.Dispose();
            }
            Clear();
            disposed = true;
        }

        protected override void RemoveItem(int index)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            base.ClearItems();
        }

        protected override void SetItem(int index, MimeAttachment item)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, MimeAttachment item)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.InsertItem(index, item);
        }
    }
}