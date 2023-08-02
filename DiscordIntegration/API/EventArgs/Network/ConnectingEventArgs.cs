namespace DiscordIntegration.API.EventArgs.Network
{
    using System;
    using System.Net;

    public class ConnectingEventArgs : EventArgs
    {
        public ConnectingEventArgs(IPAddress ipAddress, ushort port, TimeSpan reconnectionInterval)
        {
            IPAddress = ipAddress;
            Port = port;
            ReconnectionInterval = reconnectionInterval;
        }

        public IPAddress IPAddress { get; set; }
        public ushort Port { get; set; }
        public TimeSpan ReconnectionInterval { get; set; }
    }
}
