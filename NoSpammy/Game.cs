using Dalamud.Utility.Signatures;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using System.Runtime.InteropServices;
using System;

namespace NoSpammy
{
    public unsafe class Game
    {
        private delegate void ProcessChatBoxDelegate(UIModule* uiModule, nint message, nint unused, byte a4);
        [Signature("48 89 5C 24 ?? 57 48 83 EC 20 48 8B FA 48 8B D9 45 84 C9")]
        private static ProcessChatBoxDelegate ProcessChatBox = null!;

        private static UIModule* UiModule;

        public static void Initialize()
        {
            UiModule = Framework.Instance()->GetUiModule();

            SignatureHelper.Initialise(new Game());
        }
        public static void ExecuteCommand(string command)
        {
            var stringPtr = nint.Zero;
            try
            {
                stringPtr = Marshal.AllocHGlobal(UTF8String.size);
                using var str = new UTF8String(stringPtr, command);
                Marshal.StructureToPtr(str, stringPtr, false);
                ProcessChatBox(UiModule, stringPtr, nint.Zero, 0);
            }
            catch (Exception e)
            {
                PluginLog.LogError(e.Message);
            }

            Marshal.FreeHGlobal(stringPtr);
        }
    }
}
