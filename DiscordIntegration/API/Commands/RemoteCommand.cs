namespace DiscordIntegration.API.Commands
{
    using Newtonsoft.Json;
    public class RemoteCommand
    {
        [JsonConstructor]
        public RemoteCommand(string action, object parameters)
        {
            Action = action;
            Parameters = new object[1] { parameters };
        }
        public RemoteCommand(string action, params object[] parameters)
        {
            Action = action;
            Parameters = parameters;
        }
        public string Action { get; }
        public object[] Parameters { get; }
    }
}
