using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using LlamaLibrary.Logging;

namespace LlamaPlugins.UITester
{
    public partial class Form1 : Form
    {
        private static readonly LLogger Log = new LLogger("UITester", Colors.Bisque);
        private static TwoInt[] savedElements;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RaptureAtkUnitManager.Update();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            Log.Information("RaptureAtkUnitManager Updated");

            foreach (var window in RaptureAtkUnitManager.Controls.Where(i => !i.Name.StartsWith("_") && !i.Name.StartsWith("InventoryGrid") && !i.Name.StartsWith("InventoryEvent") && !i.Name.StartsWith("ChatLogPanel")))
            {
                listBox1.Items.Add($"{window.Name}");
            }

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            richTextBox1.Text = "";
            var windowName = ((string)listBox1.SelectedItem).Trim();

            Log.Information($"{windowName} Selected");
            var elements = LlamaUI.___Elements(windowName);
            if (elements == null)
            {
                return;
            }

            using (var outputFile = new StreamWriter($"{windowName}.cvs", false))
            {
                for (var j = 0; j < elements.Length; j++)
                {
                    var twoInt = elements[j];
                    string data;
                    if (twoInt.Type == 6 || twoInt.Type == 8 || twoInt.Type == 38)
                    {
                        data = Core.Memory.ReadString((IntPtr)twoInt.Data, Encoding.UTF8);
                    }

                    //listBox3.Items.Add($"[{j}:{i.Type}] ({tstring})");
                    else if (twoInt.Type == 4)
                    {
                        data = $"{twoInt.TrimmedData}";
                    }
                    else
                    {
                        data = $"{twoInt.Data:X}({twoInt.TrimmedData})";
                    }

                    listBox2.Items.Add($"[{j}:{twoInt.Type}] {data}");
                    IntPtr ptr = (IntPtr)twoInt.Data;
                    outputFile.WriteLine($"{j},{twoInt.Type},{twoInt.Data},{twoInt.TrimmedData},{data},{ptr.ToInt64():X}");
                    Log.Information($"{windowName}.cvs Written");
                }
            }

            AtkAddonControl windowByName = RaptureAtkUnitManager.GetWindowByName(windowName);
            if (windowByName != null)
            {
                AgentInterface test;
                try
                {
                    test = windowByName.TryFindAgentInterface();
                    richTextBox1.Text += $"Agent ID is: {test.Id}\n";
                    richTextBox1.Text += $"Agent Pointer: {test.Pointer.ToInt64():X} \nAgent Vtable: {test.VTable.ToInt64():X} \nVtableOffset {Core.Memory.GetRelative(test.VTable).ToInt64():X}\n";
                }
                catch
                {
                    // ignored
                }

                richTextBox1.Text += $"Window Pointer: {windowByName.Pointer.ToInt64():X} \nWindow Vtable: {windowByName.VTable.ToInt64():X} \nVtableOffset {Core.Memory.GetRelative(windowByName.VTable).ToInt64():X}\n";
            }

            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*using (var patternFinder = new GreyMagic.PatternFinder(Core.Memory))
            {
                IntPtr SendActionBreakpoint = patternFinder.Find("48 85 C0 74 ? 48 89 18 4C 8D 70 ? 49 8B C6 48 85 DB 74 ? 89 30");
                textBox1.Text = ($"ffxiv_dx11.exe+{Core.Memory.GetRelative(SendActionBreakpoint).ToString("X")}");
            }*/
        }

        private void BtnSaveState_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            RaptureAtkUnitManager.Update();
            var windowName = ((string)listBox1.SelectedItem).Trim();

            Log.Information($"{windowName} Selected");
            savedElements = LlamaUI.___Elements(windowName);
            btnCompare.Enabled = true;
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            RaptureAtkUnitManager.Update();
            var windowName = ((string)listBox1.SelectedItem).Trim();
            var newElements = LlamaUI.___Elements(windowName);

            for (var i = 0; i < savedElements.Length; i++)
            {
                if (savedElements[i].Data == newElements[i].Data)
                {
                    continue;
                }

                var twoInt = newElements[i];
                string data;
                string oldData = $"{savedElements[i].TrimmedData}";
                if (twoInt.Type == 6 || twoInt.Type == 8 || twoInt.Type == 38)
                {
                    data = Core.Memory.ReadString((IntPtr)twoInt.Data, Encoding.UTF8);
                    oldData = Core.Memory.ReadString((IntPtr)savedElements[i].Data, Encoding.UTF8);
                }

                //listBox3.Items.Add($"[{j}:{i.Type}] ({tstring})");
                else if (twoInt.Type == 4)
                {
                    data = $"{twoInt.TrimmedData}";
                }
                else
                {
                    data = $"{twoInt.Data:X}({twoInt.TrimmedData})";
                }

                listBox3.Items.Add($"[{i}:{twoInt.Type}] {data} (old: {oldData})");
            }

            btnCompare.Enabled = false;
        }
    }
}