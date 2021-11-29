using System;
using System.Windows.Media;
using ff14bot;
using ff14bot.AClasses;
using LlamaLibrary.Logging;

namespace LlamaPlugins.Ventures
{
    public class LisbethVentures : BotPlugin
    {
        private static readonly string NameValue = "Lisbeth Ventures";

        private static readonly LLogger Log = new LLogger(NameValue, Colors.Aquamarine);
        public override string Author { get; } = "Kayla";

        public override Version Version => new Version(3, 6);

        public override string Name { get; } = NameValue;

        private static bool FoundLisbeth = false;
        private static bool FoundLL = false;
        private static string HookName = "VenturesHook";
        private static string HookName1 = "VenturesCraftCycle";

        public override bool WantButton
        {
            get { return false; }
        }

        public override string ButtonText
        {
            get { return "Toggle"; }
        }

        public override void OnButtonPress()
        {
        }

        public override void OnInitialize()
        {
        }

        public override void OnEnabled()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            Log.Information($"{NameValue} Enabled");
        }

        public override void OnDisabled()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            Log.Information($"{NameValue} Disabled");
        }

        private void OnBotStop(BotBase bot)
        {
            RemoveHooks();
        }

        private void OnBotStart(BotBase bot)
        {
            AddHooks();
        }

        private void AddHooks()
        {
            var hooks = LlamaLibrary.Helpers.Lisbeth.GetHookList();
            Log.Information($"Adding {HookName} Hook");
            if (!hooks.Contains(HookName))
            {
                LlamaLibrary.Helpers.Lisbeth.AddHook(HookName, LlamaLibrary.Retainers.HelperFunctions.CheckVentureTask);
            }

            Log.Information($"Adding {HookName1} Hook");
            if (!hooks.Contains(HookName1))
            {
                LlamaLibrary.Helpers.Lisbeth.AddCraftCycleHook(HookName, LlamaLibrary.Retainers.HelperFunctions.CheckVentureTask);
            }
        }

        private void RemoveHooks()
        {
            var hooks = LlamaLibrary.Helpers.Lisbeth.GetHookList();
            Log.Information($"Removing {HookName} Hook");
            if (hooks.Contains(HookName))
            {
                LlamaLibrary.Helpers.Lisbeth.RemoveHook(HookName);
            }

            Log.Information($"Removing {HookName1} Hook");
            if (hooks.Contains(HookName1))
            {
                LlamaLibrary.Helpers.Lisbeth.RemoveCraftCycleHook(HookName1);
            }
        }
    }
}