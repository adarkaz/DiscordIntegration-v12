﻿namespace DiscordIntegration.API.EventArgs.Network
{
    using System;

    /// <summary>
    /// Contains all informations after the network thrown an exception.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception"><inheritdoc cref="Exception"/></param>
        public ErrorEventArgs(Exception exception) => Exception = exception;

        /// <summary>
        /// Gets the thrown exception.
        /// </summary>
        public Exception Exception { get; }
    }
}
