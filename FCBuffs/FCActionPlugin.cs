using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using ff14bot;
using ff14bot.AClasses;
using LlamaLibrary;
using LlamaLibrary.Logging;

namespace LlamaPlugins.FCBuffs
{
    public class FCActionPlugin : BotPlugin
    {
        private static readonly string NameValue = "FC Buffs";

        private static readonly LLogger Log = new LLogger(NameValue, Colors.Aquamarine);
        private static readonly string HookName = "FCBuffsCraft";
        private static readonly string HookName1 = "FCBuffsReg";

        public override string Author { get; } = "Kayla";
        public static BuffSettings Settings = BuffSettings.Instance;
        private BuffSetttingsFrm settings;

        public override Version Version => new Version(3, 6);

        public override string Name { get; } = NameValue;

        public override bool WantButton => true;

        public override void OnButtonPress()
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new BuffSetttingsFrm();
            }

            try
            {
                settings.Show();
                settings.Activate();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
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
                LlamaLibrary.Helpers.Lisbeth.AddHook(HookName, BuffTask);
            }

            Log.Information($"Adding {HookName1} Hook");
            if (!hooks.Contains(HookName1))
            {
                LlamaLibrary.Helpers.Lisbeth.AddCraftCycleHook(HookName, BuffTask);
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

        public static async Task BuffTask()
        {
            uint[] FCAuras = new uint[] { 353, 354, 355, 356, 357, 360, 361, 362, 363, 364, 365, 366, 367, 368, 413, 414, 902 };
            var buffs = Core.Me.Auras.Where(i => FCAuras.Contains(i.Id)).ToList();
            if (buffs.Count() < 2)
            {
                Log.Information($"Found {buffs.Count} Buffs Active");
                Log.Information($"Calling Activate");
                await LlamaLibrary.Helpers.FreeCompanyActions.ActivateBuffs((int)BuffSettings.Instance.Buff1, (int)BuffSettings.Instance.Buff2, BuffSettings.Instance.GrandCompany);
            }
        }
    }
}