using System;

namespace net.scan.ace.SecureMail
{
    /// <summary>
    /// Describes the delivery notification options for e-mail. 
    /// </summary>
    [Flags]
    public enum SecureDeliveryNotificationOptions
    {
        /// <summary>
        /// Notify if the delivery is delayed.
        /// </summary>
        Delay = 4,
        /// <summary>
        /// Never notify. 
        /// </summary>
        Never = 0x8000000,
        /// <summary>
        /// No notification. 
        /// </summary>
        None = 0,
        /// <summary>
        /// Notify if the delivery is unsuccessful. 
        /// </summary>
        OnFailure = 2,
        /// <summary>
        /// Notify if the delivery is successful. 
        /// </summary>
        OnSuccess = 1
    }
}
