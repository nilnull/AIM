using System;

namespace net.scan.ace.SecureMail
{
    /// <summary>
    /// Specifies the priority of a SecureMailMessage.
    /// </summary>
    public enum SecureMailPriority
    {
        /// <summary>
        /// The e-mail has normal priority.
        /// </summary>
        Normal,
        /// <summary>
        /// The e-mail has low priority.
        /// </summary>
        Low,
        /// <summary>
        /// The e-mail has high priority.
        /// </summary>
        High
    }
}
