using System;
using ff14bot.AClasses;

namespace LlamaPlugins.UITester
{
    public class UiBotBase : BotPlugin
    {
        private Form1 form;
        public override string Author => "Kayla D'orden (nt153133)";
        public override Version Version => new Version(0, 69);
        public override string Name => "UI Tester";

        public override bool WantButton { get; } = true;



        public override void OnButtonPress()
        {
            form = new Form1();

            form.Show();
        }

    }
}