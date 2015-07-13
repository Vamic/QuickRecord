namespace QuickRecord
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnChangeKeys = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyTextBox = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderLocation = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.maxRecordLength = new System.Windows.Forms.NumericUpDown();
            this.showNotifications = new System.Windows.Forms.CheckBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.debugLabel = new System.Windows.Forms.Label();
            this.recordToMp3 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRecordLength)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChangeKeys
            // 
            this.btnChangeKeys.Location = new System.Drawing.Point(179, 12);
            this.btnChangeKeys.Name = "btnChangeKeys";
            this.btnChangeKeys.Size = new System.Drawing.Size(93, 22);
            this.btnChangeKeys.TabIndex = 0;
            this.btnChangeKeys.Text = "Change";
            this.btnChangeKeys.UseVisualStyleBackColor = true;
            this.btnChangeKeys.Click += new System.EventHandler(this.btnChangeKeys_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "QuickRecord v1.4";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuExit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 48);
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(116, 22);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(116, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.Location = new System.Drawing.Point(12, 14);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.ReadOnly = true;
            this.hotkeyTextBox.Size = new System.Drawing.Size(161, 20);
            this.hotkeyTextBox.TabIndex = 1;
            this.hotkeyTextBox.Text = "Ctrl+Shift+6";
            this.hotkeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hotkeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkeyTextBox_KeyDown);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 40);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(223, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Until the key combination is pressed again";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(12, 63);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(91, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Specified time";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(109, 63);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(42, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // folderLocation
            // 
            this.folderLocation.Location = new System.Drawing.Point(11, 109);
            this.folderLocation.Name = "folderLocation";
            this.folderLocation.ReadOnly = true;
            this.folderLocation.Size = new System.Drawing.Size(153, 20);
            this.folderLocation.TabIndex = 5;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(170, 107);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(30, 22);
            this.browseButton.TabIndex = 6;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(11, 86);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(153, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Save recordings to a folder";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Max record length";
            // 
            // maxRecordLength
            // 
            this.maxRecordLength.Location = new System.Drawing.Point(110, 130);
            this.maxRecordLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxRecordLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxRecordLength.Name = "maxRecordLength";
            this.maxRecordLength.Size = new System.Drawing.Size(54, 20);
            this.maxRecordLength.TabIndex = 9;
            this.maxRecordLength.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.maxRecordLength.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // showNotifications
            // 
            this.showNotifications.AutoSize = true;
            this.showNotifications.Checked = true;
            this.showNotifications.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNotifications.Location = new System.Drawing.Point(12, 153);
            this.showNotifications.Name = "showNotifications";
            this.showNotifications.Size = new System.Drawing.Size(112, 17);
            this.showNotifications.TabIndex = 10;
            this.showNotifications.Text = "Show notifications";
            this.showNotifications.UseVisualStyleBackColor = true;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(221, 107);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(51, 22);
            this.openFolderButton.TabIndex = 11;
            this.openFolderButton.Text = "Open";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(176, 154);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(0, 13);
            this.debugLabel.TabIndex = 12;
            // 
            // recordToMp3
            // 
            this.recordToMp3.AutoSize = true;
            this.recordToMp3.Checked = true;
            this.recordToMp3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recordToMp3.Location = new System.Drawing.Point(11, 176);
            this.recordToMp3.Name = "recordToMp3";
            this.recordToMp3.Size = new System.Drawing.Size(96, 17);
            this.recordToMp3.TabIndex = 13;
            this.recordToMp3.Text = "Record to mp3";
            this.recordToMp3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 201);
            this.Controls.Add(this.recordToMp3);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.showNotifications);
            this.Controls.Add(this.maxRecordLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.folderLocation);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.hotkeyTextBox);
            this.Controls.Add(this.btnChangeKeys);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Settings";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxRecordLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangeKeys;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.TextBox hotkeyTextBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox folderLocation;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxRecordLength;
        private System.Windows.Forms.CheckBox showNotifications;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.CheckBox recordToMp3;
    }
}

