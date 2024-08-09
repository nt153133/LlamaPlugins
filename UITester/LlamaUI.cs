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

#if RB_DT
        private const int offset0 = 0x1DA; //0F BF 93 ? ? ? ? 41 B1 ? 4C 8B 83 ? ? ? ? 48 8B CB C6 44 24 ? ? E8 ? ? ? ? 48 8B CB Add 3 Read32
        private const int offset2 = 0x170; //4C 8B 83 ? ? ? ? 48 8B CB C6 44 24 ? ? E8 ? ? ? ? 48 8B CB Add 3 Read32
#else
        private const int offset0 = 0x1CA;
        private const int offset2 = 0x160;
#endif

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