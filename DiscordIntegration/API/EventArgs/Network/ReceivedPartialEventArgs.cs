﻿namespace DiscordIntegration.API.EventArgs.Network
{
    /// <summary>
    /// Contains all informations after the network received partial data.
    /// </summary>
    public class ReceivedPartialEventArgs : ReceivedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedPartialEventArgs"/> class.
        /// </summary>
        /// <param name="data"><inheritdoc cref="ReceivedEventArgs.Data"/></param>
        /// <param name="length"><inheritdoc cref="ReceivedEventArgs.Length"/></param>
        public ReceivedPartialEventArgs(string data, int length)
            : base(data, length)
        {
        }
    }
}
