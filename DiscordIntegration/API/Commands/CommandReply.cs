namespace DiscordIntegration.API.Commands
{
    using Newtonsoft.Json;

    public class CommandReply
    {
        [JsonConstructor]
        public CommandReply(CommandSender sender, string response, bool isSucceeded)
        {
            Sender = sender;
            Response = response;
            IsSucceeded = isSucceeded;
        }
        public CommandSender Sender { get; }
        public string Response { get; }
        public bool IsSucceeded { get; }
        public void Answer() => Sender?.RaReply(Response, IsSucceeded, true, string.Empty);
    }
}
