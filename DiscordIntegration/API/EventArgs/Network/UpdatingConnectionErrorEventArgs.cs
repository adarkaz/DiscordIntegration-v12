namespace DiscordIntegration.API.EventArgs.Network
{
    using System;

    /// <summary>
    /// Contains all informations after the network thrown an exception while updating the connection.
    /// </summary>
    public class UpdatingConnectionErrorEventArgs : ErrorEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatingConnectionErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception"><inheritdoc cref="ErrorEventArgs.Exception"/></param>
        public UpdatingConnectionErrorEventArgs(Exception exception)
            : base(exception)
        {
        }
    }
}
