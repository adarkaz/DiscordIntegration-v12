namespace DiscordIntegration.API.EventArgs.Network
{
    using System;

    /// <summary>
    /// Contains all informations after the network thrown an exception while sending data.
    /// </summary>
    public class SendingErrorEventArgs : ErrorEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendingErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception"><inheritdoc cref="ErrorEventArgs.Exception"/></param>
        public SendingErrorEventArgs(Exception exception)
            : base(exception)
        {
        }
    }
}
