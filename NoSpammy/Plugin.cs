using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using NoSpammy.Windows;

namespace NoSpammy
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "No Spammy";
        private const string CommandName = "/nospam";
        private const string ConfigCommandName = "/nospamcfg";

        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("No Spammy");

        public Debouncer Debouncer = new Debouncer();

        private ConfigWindow ConfigWindow { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager)
        {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;

            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginInterface);

            Game.Initialize();

            ConfigWindow = new ConfigWindow(this);
            
            WindowSystem.AddWindow(ConfigWindow);

            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Runs a command if the same command has not been run recently."
                    + "\n\t/nospam echo You can't spam this! <se.9>"
            });


            CommandManager.AddHandler(ConfigCommandName, new CommandInfo((command, args) => OpenConfigUI())
            {
                HelpMessage = "Opens the plugin configuration window."
            });

            PluginInterface.UiBuilder.Draw += WindowSystem.Draw;
            PluginInterface.UiBuilder.OpenConfigUi += OpenConfigUI;
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
            
            ConfigWindow.Dispose();
            
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            var milliseconds = Configuration.DebounceTime;
            if (Debouncer.Debounce(args, milliseconds))
            {
                Game.ExecuteCommand("/" + args);
            }
        }

        public void OpenConfigUI()
        {
            ConfigWindow.IsOpen = true;
        }
    }
}
