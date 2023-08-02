namespace DiscordIntegration.API.EventArgs.Network
{
    using System;

    /// <summary>
    /// Contains all informations after the network thrown an exception while connecting data.
    /// </summary>
    public class ConnectingErrorEventArgs : ErrorEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectingErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception"><inheritdoc cref="ErrorEventArgs.Exception"/></param>
        public ConnectingErrorEventArgs(Exception exception)
            : base(exception)
        {
        }
    }
}
