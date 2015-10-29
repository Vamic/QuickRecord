using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Hotkeys;
using System.Globalization;

namespace QuickRecord
{

    public partial class Form1 : Form
    {
        NAudio.Wave.WasapiLoopbackCapture sourceStream;
        NAudio.Lame.LameMP3FileWriter mp3Writer;
        NAudio.Wave.WaveFileWriter waveWriter;
        GlobalHotkey ghk;
        bool recording = false;
        string hotkeystring = "";
        string path = "";
        string filename = "";
        string filetype = "";
        Timer timer;

        Keys modifierKeys;
        Keys pressedKey;
        public Form1()
        {
            InitializeComponent();
            
            timer = new Timer(); //a timer for stopping recording at a certain time
            timer.Interval = 120000; //max recording of 120000ms (2 minutes)
            timer.Tick += new EventHandler(timer_Tick);

            if (Properties.Settings.Default.upgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.upgradeRequired = false;
            }

            //if untouched, set default values
            if (Properties.Settings.Default.unTouched)
            {
                Properties.Settings.Default.unTouched = false;
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
                recordToMp3.Checked = Properties.Settings.Default.recordToMp3;

                folderLocation.Text = Properties.Settings.Default.folderLocation;

                saveFormat.Text = Properties.Settings.Default.saveFormat;
                incrementNumber.Value = Properties.Settings.Default.incrementNumber;
            }
            ghk = new GlobalHotkey(Constants.ToInt(modifierKeys), pressedKey, this);
            ghk.Register();

            hotkeyTextBox.Text = new KeysConverter().ConvertToString(modifierKeys ^ pressedKey);

            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = "QuickRecord";
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.modifierKeys = modifierKeys;

            Properties.Settings.Default.pressedKey = pressedKey;

            Properties.Settings.Default.showNotifications = showNotifications.Checked;

            Properties.Settings.Default.recordToMp3 = recordToMp3.Checked;

            Properties.Settings.Default.maxRecordLength = maxRecordLength.Value;

            Properties.Settings.Default.folderLocation = folderLocation.Text;

            Properties.Settings.Default.saveFormat = saveFormat.Text;

            Properties.Settings.Default.incrementNumber = (int)incrementNumber.Value;

            Properties.Settings.Default.Save();
        }

        private void HandleHotkey()
        {
            if (!recording)
            {
                sourceStream = new NAudio.Wave.WasapiLoopbackCapture();
                sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                path = folderLocation.Text + "\\";
                filename = GetFileName(false);
                if (recordToMp3.Checked)
                {
                    filetype = ".mp3";
                    mp3Writer = new NAudio.Lame.LameMP3FileWriter(filename + filetype, sourceStream.WaveFormat, 128);
                }
                else
                {
                    filetype = ".wav";
                    waveWriter = new NAudio.Wave.WaveFileWriter(filename + filetype, sourceStream.WaveFormat);
                }
                
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

        private string GetFileName(bool preview)
        {
            string filename = "";
            string format = saveFormat.Text;
            DateTime dt = DateTime.Now;

            //return default if format is empty
            if (string.IsNullOrEmpty(format))
            {
                format = "%y-%mo-%d %h-%mi-%s QuickRecording";
            }

            //remove illegal characters
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                format = format.Replace(c.ToString(), "");
            }

            StringBuilder sb = new StringBuilder(format);
            
            sb.Replace("%mon2", CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace("%mon", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month))
                .Replace("%yy", dt.ToString("yy"))
                .Replace("%y", dt.Year.ToString())
                .Replace("%mo", Helpers.AddZeroes(dt.Month))
                .Replace("%d", Helpers.AddZeroes(dt.Day));

            string hour;

            if (sb.ToString().Contains("%pm"))
            {
                hour = Helpers.HourTo12(dt.Hour);
            }
            else
            {
                hour = Helpers.AddZeroes(dt.Hour);
            }

            sb.Replace("%h", hour)
                .Replace("%mi", Helpers.AddZeroes(dt.Minute))
                .Replace("%s", Helpers.AddZeroes(dt.Second))
                .Replace("%ms", Helpers.AddZeroes(dt.Millisecond, 3))
                .Replace("%w2", CultureInfo.InvariantCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                .Replace("%w", CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dt.DayOfWeek))
                .Replace("%pm", (dt.Hour >= 12 ? "PM" : "AM"));
            
            if (sb.ToString().Contains("%i"))
            {
                if (!preview)
                {
                    incrementNumber.Value++;
                }
                sb.Replace("%i", incrementNumber.Value.ToString());
                sb.Replace("%i", incrementNumber.Value.ToString());
            }

            sb.Replace("%un", Environment.UserName);
            sb.Replace("%uln", Environment.UserDomainName);
            sb.Replace("%cn", Environment.MachineName);
            
            filename = sb.ToString();

            if(Properties.Settings.Default.incrementNumber != incrementNumber.Value
                    || Properties.Settings.Default.saveFormat != saveFormat.Text)
                SaveSettings();

            return filename;
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
            if (mp3Writer != null)
                mp3Writer.Dispose();
            if (waveWriter != null)
                waveWriter.Dispose();
            recording = false;
            timer.Stop();

            File.Move(filename + filetype, path + filename + filetype);

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
            if (recordToMp3.Checked)
            {
                if (mp3Writer == null || !mp3Writer.CanWrite)
                    return;
                
                mp3Writer.Write(e.Buffer, 0, e.BytesRecorded);
            }
            else
            {
                if (waveWriter == null)
                    return;
                waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveWriter.Flush();
            }
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
            if (mp3Writer != null)
            {
                mp3Writer.Dispose();
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

        private void saveFormat_TextChanged(object sender, EventArgs e)
        {
            labelFileName.Text = GetFileName(true);
            SaveSettings();
        }

        private void incrementNumber_ValueChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }
        #endregion
    }
}
