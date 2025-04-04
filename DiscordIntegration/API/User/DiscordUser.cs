﻿namespace DiscordIntegration.API.User
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents a Discord user.
    /// </summary>
    public class DiscordUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordUser"/> class.
        /// </summary>
        /// <param name="id"><inheritdoc cref="Id"/></param>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        [JsonConstructor]
        public DiscordUser(string id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets the Discord user ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the Discord username.
        /// </summary>
        public string Name { get; }
    }
}
