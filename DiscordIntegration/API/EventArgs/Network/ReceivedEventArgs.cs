namespace DiscordIntegration.API.EventArgs.Network
{
    using System;
    using System.Text;

    /// <summary>
    /// Contains all informations after the network received data.
    /// </summary>
    public class ReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data"><inheritdoc cref="Data"/></param>
        /// <param name="length"><inheritdoc cref="Length"/></param>
        public ReceivedEventArgs(string data, int length)
        {
            Data = data;
            Length = length;
        }

        /// <summary>
        /// Gets the received data.
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// Gets the received bytes length.
        /// </summary>
        public int Length { get; }
    }
}
