using System;
using System.Windows.Forms;

namespace LlamaPlugins.Repair
{
    public partial class RepairSettingsFrm : Form
    {
        public RepairSettingsFrm()
        {
            InitializeComponent();
        }

        private void RepairSettingsFrm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = RepairSettings.Instance;
        }
    }
}