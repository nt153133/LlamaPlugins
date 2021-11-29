using System.ComponentModel;
using System.IO;
using ff14bot.Helpers;
using LlamaLibrary.Helpers;

namespace LlamaPlugins.Repair
{
    public class RepairSettings : JsonSettings
    {
        private static RepairSettings _settings;

        public static RepairSettings Instance => _settings ?? (_settings = new RepairSettings());

        private int _repairThreshold;

        public RepairSettings() : base(Path.Combine(JsonHelper.UniqueCharacterSettingsDirectory, "RepairSettings.json"))
        {
        }

        [Description("Lisbeth repair threshold")]
        [DefaultValue(0)] //shift +x
        public int RepairThreshold
        {
            get => _repairThreshold;
            set
            {
                if (_repairThreshold != value)
                {
                    _repairThreshold = value;
                    Save();
                }
            }
        }
    }
}