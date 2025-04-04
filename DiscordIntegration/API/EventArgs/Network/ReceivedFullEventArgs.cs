﻿namespace DiscordIntegration.API.EventArgs.Network
{
    /// <summary>
    /// Contains all informations after the network received full data.
    /// </summary>
    public class ReceivedFullEventArgs : ReceivedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedFullEventArgs"/> class.
        /// </summary>
        /// <param name="data"><inheritdoc cref="ReceivedEventArgs.Data"/></param>
        /// <param name="length"><inheritdoc cref="ReceivedEventArgs.Length"/></param>
        public ReceivedFullEventArgs(string data, int length)
            : base(data, length)
        {
        }
    }
}
