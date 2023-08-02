namespace DiscordIntegration.API.Commands
{
    using API.User;
    using Newtonsoft.Json;
    using RemoteAdmin;
    public class GameCommand
    {
        [JsonConstructor]
        public GameCommand(string channelId, string content, DiscordUser user)
        {
            ChannelId = channelId;
            Content = content;
            User = user;
            Sender = new BotCommandSender(channelId, user?.Id, user?.Name);
        }
        public string ChannelId { get; }
        public string Content { get; }
        public DiscordUser User { get; }
        [JsonIgnore]
        public CommandSender Sender { get; }
        public void Execute() => CommandProcessor.ProcessQuery(Content, Sender);
    }
}
