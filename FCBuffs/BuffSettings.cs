using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ff14bot.Enums;
using ff14bot.Helpers;
using LlamaLibrary;

namespace LlamaPlugins.FCBuffs
{
    public class BuffSettings : JsonSettings
    {
        private static BuffSettings _settings;

        public static BuffSettings Instance => _settings ?? (_settings = new BuffSettings());

        private FCActions _buff1;
        private FCActions _buff2;
        private GrandCompany _grandCompany;

        public static Dictionary<uint, string> FcActionList = new Dictionary<uint, string>()
        {
            { 1, "The Heat of Battle" },
            { 2, "Earth and Water" },
            { 3, "Helping Hand" },
            { 4, "A Man's Best Friend" },
            { 5, "Mark Up" },
            { 6, "Seal Sweetener" },
            { 7, "Brave New World" },
            { 8, "Live off the Land" },
            { 9, "What You See" },
            { 10, "Eat from the Hand" },
            { 11, "In Control" },
            { 12, "That Which Binds Us" },
            { 13, "Meat and Mead" },
            { 14, "Proper Care" },
            { 15, "Back on Your Feet" },
            { 16, "Reduced Rates" },
            { 17, "Jackpot" },
            { 31, "The Heat of Battle II" },
            { 32, "Earth and Water II" },
            { 33, "Helping Hand II" },
            { 34, "A Man's Best Friend II" },
            { 35, "Mark Up II" },
            { 36, "Seal Sweetener II" },
            { 37, "Brave New World II" },
            { 38, "Live off the Land II" },
            { 39, "What You See II" },
            { 40, "Eat from the Hand II" },
            { 41, "In Control II" },
            { 42, "That Which Binds Us II" },
            { 43, "Meat and Mead II" },
            { 44, "Proper Care II" },
            { 45, "Back on Your Feet II" },
            { 46, "Reduced Rates II" },
            { 47, "Jackpot II" },
            { 61, "The Heat of Battle III" },
            { 62, "Earth and Water III" },
            { 63, "Helping Hand III" },
            { 64, "A Man's Best Friend III" },
            { 65, "Mark Up III" },
            { 66, "Seal Sweetener III" },
            { 68, "Live off the Land III" },
            { 69, "What You See III" },
            { 70, "Eat from the Hand III" },
            { 71, "In Control III" },
            { 72, "That Which Binds Us III" },
            { 73, "Meat and Mead III" },
            { 74, "Proper Care III" },
            { 76, "Reduced Rates III" },
            { 77, "Jackpot III" },
        };

        public BuffSettings() : base(Path.Combine(CharacterSettingsDirectory, "BuffSettings.json"))
        {
        }

        [Description("FC Buff #1")]
        [DefaultValue(FCActions.The_Heat_of_Battle_II)] //shift +x
        public FCActions Buff1
        {
            get => _buff1;
            set
            {
                if (_buff1 != value)
                {
                    _buff1 = value;
                    Save();
                }
            }
        }

        [Description("FC Buff #2")]
        [DefaultValue(FCActions.In_Control_II)] //shift +x
        public FCActions Buff2
        {
            get => _buff2;
            set
            {
                if (_buff2 != value)
                {
                    _buff2 = value;
                    Save();
                }
            }
        }

        [Description("FC GrandCompany Alliance")]
        [DefaultValue(ff14bot.Enums.GrandCompany.Maelstrom)] //shift +x
        public GrandCompany GrandCompany
        {
            get => _grandCompany;
            set
            {
                if (_grandCompany != value)
                {
                    _grandCompany = value;
                    Save();
                }
            }
        }
    }
}