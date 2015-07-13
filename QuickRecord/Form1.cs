using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio;
using Hotkeys;

namespace QuickRecord
{

    public partial class Form1 : Form
    {
        NAudio.Wave.WasapiLoopbackCapture sourceStream;
        NAudio.Wave.WaveFileWriter waveWriter;
        Hotkeys.GlobalHotkey ghk;
        bool recording;
        string pathend, hotkeystring;
        Timer timer;

        Keys modifierKeys;
        Keys pressedKey;
        public Form1()
        {
            InitializeComponent();

            timer = new Timer(); //a timer for stopping recording at a certain time
            timer.Interval = 120000; //max recording of 120000ms (2 minutes)
            timer.Tick += new EventHandler(timer_Tick);

            if (Properties.Settings.Default.modifierKeys == Keys.None)
            {
                modifierKeys = Keys.Shift ^ Keys.Control;
                pressedKey = Keys.D6;
                folderLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                SaveSettings();
            }
            else
            {
                modifierKeys = Properties.Settings.Default.modifierKeys;
                pressedKey = Properties.Settings.Default.pressedKey;
                maxRecordLength.Value = Properties.Settings.Default.maxRecordLength;
                showNotifications.Checked = Properties.Settings.Default.showNotifications;
                folderLocation.Text = Properties.Settings.Default.folderLocation;
            }
            ghk = new GlobalHotkey(Constants.ToInt(modifierKeys), pressedKey, this);
            ghk.Register();

            hotkeyTextBox.Text = new KeysConverter().ConvertToString(modifierKeys ^ pressedKey);

            pathend = "QuickRecording" + ".wav";

            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "QuickRecord";
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.modifierKeys = modifierKeys;
            Properties.Settings.Default.pressedKey = pressedKey;

            Properties.Settings.Default.showNotifications = showNotifications.Checked;
            Properties.Settings.Default.maxRecordLength = maxRecordLength.Value;

            Properties.Settings.Default.folderLocation = folderLocation.Text;

            Properties.Settings.Default.Save();
        }

        private void HandleHotkey()
        {
            if (!recording)
            {
                sourceStream = new NAudio.Wave.WasapiLoopbackCapture();
                sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                string pathstart = folderLocation.Text + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss ");
                waveWriter = new NAudio.Wave.WaveFileWriter(pathstart+pathend, sourceStream.WaveFormat);

                sourceStream.StartRecording();
                recording = true;
                timer.Start();

                notifyIcon.BalloonTipText = "Started recording.";
                if (showNotifications.Checked)
                    notifyIcon.ShowBalloonTip(1000);

            }
            else
            {
                StopRecording();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (recording)
            {
                StopRecording();
            }
        }

        private void StopRecording()
        {
            sourceStream.StopRecording();
            sourceStream.Dispose();
            waveWriter.Dispose();
            recording = false;
            timer.Stop();
            notifyIcon.BalloonTipText = "Recording finished.";
            if (showNotifications.Checked)
                notifyIcon.ShowBalloonTip(1000);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                HandleHotkey();
            }
            base.WndProc(ref m);
        }
        
        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null)
                return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void btnChangeKeys_Click(object sender, EventArgs e)
        {
            if (btnChangeKeys.Text == "Change")
            {
                hotkeyTextBox.Text = "";
                hotkeyTextBox.Select();
                btnChangeKeys.Text = "Save";
            }
            else
            {
                if (hotkeyTextBox.Text == "")
                {
                    hotkeyTextBox.Text = hotkeystring;
                }
                else
                {
                    ghk.Unregister();
                    ghk = new GlobalHotkey(Constants.ToInt(modifierKeys), pressedKey, this);
                    ghk.Register();
                }
                hotkeystring = hotkeyTextBox.Text;
                btnChangeKeys.Text = "Change";

                SaveSettings();
            }
        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnChangeKeys.Text != "Change")
            {
                //Get modifier keys
                Keys tmodifierKeys = e.Modifiers;
                //Get the other key
                Keys tpressedKey = e.KeyData ^ tmodifierKeys;
                if (tmodifierKeys != Keys.None && tpressedKey != Keys.None)
                {
                    //ignore the modifier keys
                    if (tpressedKey != Keys.Menu
                        && tpressedKey != Keys.ShiftKey
                        && tpressedKey != Keys.ControlKey)
                    {
                        //show the user his key inputs
                        hotkeyTextBox.Text = new KeysConverter().ConvertToString(e.KeyData);
                        modifierKeys = tmodifierKeys;
                        pressedKey = tpressedKey;
                    }
                }
                
            }
        }

        private void Shutdown()
        {
            SaveSettings();

            ghk.Unregister();

            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
            }

            Environment.Exit(0);
        }
        #region Boring Shit


        private void menuExit_Click(object sender, EventArgs e)
        {
            Shutdown();
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            timer.Interval = (int)maxRecordLength.Value*1000;
            SaveSettings();
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            Hide();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderLocation.Text = folderBrowserDialog1.SelectedPath;
                SaveSettings();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //browseButton.Enabled = checkBox1.Checked;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", folderLocation.Text);
        }
        #endregion
    }
}
