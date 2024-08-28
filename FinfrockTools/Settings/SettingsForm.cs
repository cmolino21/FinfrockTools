using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinfrockTools.Settings
{
    public class SettingsForm : Form
    {
        private CheckBox chkWorksetPrompt;
        private Button btnOK;
        private Panel panel1;
        private Label lblBuildVersion;

        public SettingsForm(bool isWorksetPromptEnabled, string buildVersion)
        {
            InitializeComponent();
            chkWorksetPrompt.Checked = isWorksetPromptEnabled;
            lblBuildVersion.Text = $"Build Version: {buildVersion}";

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.chkWorksetPrompt = new System.Windows.Forms.CheckBox();
            this.lblBuildVersion = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkWorksetPrompt
            // 
            this.chkWorksetPrompt.AutoSize = true;
            this.chkWorksetPrompt.Location = new System.Drawing.Point(15, 32);
            this.chkWorksetPrompt.Name = "chkWorksetPrompt";
            this.chkWorksetPrompt.Size = new System.Drawing.Size(138, 17);
            this.chkWorksetPrompt.TabIndex = 0;
            this.chkWorksetPrompt.Text = "Enable Workset Prompt";
            this.chkWorksetPrompt.UseVisualStyleBackColor = true;
            // 
            // lblBuildVersion
            // 
            this.lblBuildVersion.AutoSize = true;
            this.lblBuildVersion.Location = new System.Drawing.Point(12, 9);
            this.lblBuildVersion.Name = "lblBuildVersion";
            this.lblBuildVersion.Size = new System.Drawing.Size(74, 13);
            this.lblBuildVersion.TabIndex = 1;
            this.lblBuildVersion.Text = "Build Version: ";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(155, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 31);
            this.panel1.TabIndex = 3;
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(237, 102);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkWorksetPrompt);
            this.Controls.Add(this.lblBuildVersion);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public bool IsWorksetPromptEnabled()
        {
            return chkWorksetPrompt.Checked;
        }

        // Handle the OK button click event
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Overriding the OnFormClosing method to ensure settings are saved
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SaveSettings();
        }

        // Method to save the settings
        private void SaveSettings()
        {
            // Update the settings with the current values from the form
            App.Settings.IsWorksetPromptEnabled = chkWorksetPrompt.Checked;

            // Save the settings to the XML file
            App.Settings.Save();
        }

    }

}
