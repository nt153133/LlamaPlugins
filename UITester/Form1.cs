using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using ff14bot;
using ff14bot.Managers;
using LlamaLibrary.Logging;

namespace LlamaPlugins.UITester
{
    public partial class Form1 : Form
    {
        private static readonly LLogger Log = new LLogger("UITester", Colors.Bisque);

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
                    var i = elements[j];
                    string data;
                    if (i.Type == 6 || i.Type == 8 || i.Type == 38)
                    {
                        data = Core.Memory.ReadString((IntPtr)i.Data, Encoding.UTF8);
                    }

                    //listBox3.Items.Add($"[{j}:{i.Type}] ({tstring})");
                    else if (i.Type == 4)
                    {
                        data = $"{i.TrimmedData}";
                    }
                    else
                    {
                        data = $"{i.Data:X}({i.TrimmedData})";
                    }

                    listBox2.Items.Add($"[{j}:{i.Type}] {data}");
                    IntPtr ptr = (IntPtr)i.Data;
                    outputFile.WriteLine($"{j},{i.Type},{i.Data},{i.TrimmedData},{data},{ptr.ToInt64():X}");
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
    }
}