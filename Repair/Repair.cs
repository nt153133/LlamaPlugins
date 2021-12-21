/*
AutoRepair is licensed under a
Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc-sa/4.0/>.
Original work done by Kayla D'orden
                                                                                 */

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using ff14bot.Helpers;
using ff14bot.Managers;
using TreeSharp;
using Action = TreeSharp.Action;

namespace LlamaPlugins.Repair
{
    public class AutoRepair : BotPlugin
    {
        private static ActionRunCoroutine hook;
        private object _lisbeth;
        private MethodInfo _orderMethod;

        private Composite _root;
        public override string Author => "Kayla D'orden";
        public override Version Version => new Version(1, 7);
        public override string Name => _Name;
        public override bool WantButton => true;
        private RepairSettingsFrm settings;

        public override void OnPulse()
        {
        }

        public override void OnButtonPress()
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new RepairSettingsFrm();
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

        public static string _Name => "AutoRepairLisbeth";

        public Composite CreateVendorBehavior
        {
            get
            {
                return new Decorator(
                    r => !Core.Me.InCombat && !MovementManager.IsOccupied && !Repairing,
                    new PrioritySelector(new Decorator(
                                         r => InventoryManager.EquippedItems.Any(item => item.Item != null && item.Item.RepairItemId != 0 && item.Condition < RepairSettings.Instance.RepairThreshold),
                                         new Sequence(
                                                                            new Action(r => Log("Start")),
                                                                            new Action(r => TreeRoot.StatusText = "Should be repairing"),
                                                                            new ActionRunCoroutine(SelfRepair),

                                                                            // new Action(async r => await SelfRepair()),
                                                                            new Action(r => TreeRoot.StatusText = "Should be done repairing")))));
            }
        }

        public static bool Repairing { get; set; }

        public override void OnInitialize()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            Repairing = false;
        }

        private void OnBotStop(BotBase bot)
        {
            RemoveHooks();
        }

        private void OnBotStart(BotBase bot)
        {
            AddHooks();
        }

        public override void OnShutdown()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();
        }

        private void AddHooks()
        {
            Log("Adding Hook");
            Repairing = false;

            TreeHooks.Instance.AddHook("TreeStart", CreateVendorBehavior);
        }

        public async Task<bool> SelfRepair(object o)
        {
            Log("Calling Lisbeth");
            await LlamaLibrary.Helpers.Lisbeth.SelfRepairWithMenderFallback();
            return true;
        }

        private void RemoveHooks()
        {
            Log("Removing Hook");
            Repairing = false;
            TreeHooks.Instance.RemoveHook("TreeStart", CreateVendorBehavior);
        }

        private void OnHooksCleared(object sender, EventArgs e)
        {
            AddHooks();
        }

        public override void OnEnabled()
        {
            Repairing = false;
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            TreeHooks.Instance.OnHooksCleared += OnHooksCleared;

            if (TreeRoot.IsRunning)
            {
                AddHooks();
            }
        }

        public override void OnDisabled()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();
        }

        public static void Log(string text)
        {
            Logging.Write(Colors.OrangeRed, string.Format("[{1}] {0}", text, _Name));
        }
    }
}