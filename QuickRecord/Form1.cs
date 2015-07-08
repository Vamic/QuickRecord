using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        string pathend;
        Timer timer;

        public Form1()
        {
            InitializeComponent();
            ghk = new GlobalHotkey(6, Keys.D6, this);
            ghk.Register();
            timer = new Timer();
            timer.Interval = 120000; //max recording of 120000ms (2 minutes) just in case, should be changable maybe
            timer.Tick += new EventHandler(timer_Tick);


            pathend = "QuickRecording" + ".wav";


            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "QuickRecord";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            folderLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void HandleHotkey()
        {
            if (!recording)
            {
                sourceStream = new NAudio.Wave.WasapiLoopbackCapture();
                sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                string pathstart = folderLocation.Text + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
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
                hotkeyTextBox.SelectAll();
                btnChangeKeys.Text = "Save";
            }
            else
            {
                if (hotkeyTextBox.Text == "")
                {
                    hotkeyTextBox.Text = "Ctrl+Shift+6";
                }
                btnChangeKeys.Text = "Change";
            }
        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnChangeKeys.Text != "Change")
            {
                Keys modifierKeys = e.Modifiers;

                Keys pressedKey = e.KeyData ^ modifierKeys ^ Keys.ShiftKey;

                if (modifierKeys != Keys.None && pressedKey != Keys.None)
                {
                    if (pressedKey != Keys.LButton && pressedKey != Keys.RButton)
                    {
                        var converter = new KeysConverter();
                        hotkeyTextBox.Text = converter.ConvertToString(e.KeyData);
                    }
                }
            }
        }

        #region Boring Shit

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

        private void menuExit_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void Shutdown()
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                folderLocation.Text = folderBrowserDialog1.SelectedPath;
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
