using System;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;

namespace LlamaPlugins.UITester
{
    public static class LlamaUI
    {
        static LlamaUI()
        {

        }

        private static string Name => "LlamaUI";
        private static int offset0 = 458;
        private static int offset2 = 352;


        public static TwoInt[] ___Elements(string name)
        {

            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(name);
            if (windowByName != null)
            {
                ushort elementCount = ElementCount(name);

                IntPtr addr = Core.Memory.Read<IntPtr>(windowByName.Pointer + offset2);
                return Core.Memory.ReadArray<TwoInt>(addr, elementCount);
            }
            return null;

        }

        public static ushort ElementCount(string name)
        {

            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(name);
            if (windowByName != null)
            {
                return Core.Memory.Read<ushort>(windowByName.Pointer + offset0);
            }
            return 0;

        }
    }
}