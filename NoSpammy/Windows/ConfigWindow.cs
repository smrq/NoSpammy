using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace NoSpammy.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration configuration;

    public ConfigWindow(Plugin plugin) : base(
        "NoSpammy",
        ImGuiWindowFlags.AlwaysAutoResize)
    {
        configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        ImGui.SetNextItemWidth(75);

        var debounceTime = configuration.DebounceTime;
        if (ImGui.DragInt("Debounce time (milliseconds)", ref debounceTime))
        {
            configuration.DebounceTime = debounceTime;
            configuration.Save();
        }
    }
}
