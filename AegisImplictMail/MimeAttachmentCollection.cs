using System;
using System.Collections.ObjectModel;



namespace AegisImplicitMail
{
    /// <summary>
    /// A collection of SmtpAttachments
    /// </summary>
    public class MimeAttachmentCollection : Collection<MimeAttachment>, IDisposable
    {
        private bool disposed = false;

        internal MimeAttachmentCollection()
        {
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            foreach (MimeAttachment attachment in this)
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
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            base.ClearItems();
        }

        protected override void SetItem(int index, MimeAttachment item)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
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
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.InsertItem(index, item);
        }
    }

}
