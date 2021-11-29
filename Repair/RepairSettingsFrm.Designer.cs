using System.ComponentModel;

namespace LlamaPlugins.Repair
{
    partial class RepairSettingsFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            //
            // propertyGrid1
            //
            this.propertyGrid1.Location = new System.Drawing.Point(1, 9);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(354, 238);
            this.propertyGrid1.TabIndex = 0;
            //
            // RepairSettingsFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 248);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "RepairSettingsFrm";
            this.Text = "RepairSettingsFrm";
            this.Load += new System.EventHandler(this.RepairSettingsFrm_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PropertyGrid propertyGrid1;

        #endregion
    }
}