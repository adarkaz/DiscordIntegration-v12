﻿namespace DiscordIntegration.API.EventArgs.Network
{
    /// <summary>
    /// Contains all informations after the network sent data.
    /// </summary>
    public class SentEventArgs : ReceivedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SentEventArgs"/> class.
        /// </summary>
        /// <param name="data"><inheritdoc cref="ReceivedEventArgs.Data"/></param>
        /// <param name="length"><inheritdoc cref="ReceivedEventArgs.Length"/></param>
        public SentEventArgs(string data, int length)
            : base(data, length)
        {
        }
    }
}
