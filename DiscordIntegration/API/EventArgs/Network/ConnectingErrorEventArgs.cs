namespace DiscordIntegration.API.EventArgs.Network;
using System;

public class ConnectingErrorEventArgs : ErrorEventArgs
{
    public ConnectingErrorEventArgs(Exception exception)
        : base(exception)
    {
    }
}