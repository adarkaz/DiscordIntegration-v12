namespace DiscordIntegration.Commands
{
    using System;
    using System.Linq;
    using CommandSystem;
    using static DiscordIntegration;

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    internal sealed class Main : ParentCommand
    {
        public Main() => LoadGeneratedCommands();

        public override string Command { get; } = "discordintegration";

        public override string[] Aliases { get; } = new[] { "di" };

        public override string Description { get; } = string.Empty;

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new PlayerList());
            RegisterCommand(new StaffList());
            RegisterCommand(Reload.Reload.Instance);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"{Language.InvalidSubcommand} {Language.Available}: {string.Join(", ", Commands.Values.Select(x => x.Command))}"; // playerlist, stafflist, reload, add, remove";
            return false;
        }
    }
}
