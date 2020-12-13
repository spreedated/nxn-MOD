using neXn.MOD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TestPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Player player;
        private readonly string trackPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", string.Empty), @"..\..\..\NUnitTests\TestFiles"));
        private void Form1_Load(object sender, EventArgs e)
        {
            BTN_Load.Text = "\u23CF";
            BTN_Pause.Text = "\u23F8";
            BTN_Play.Text = "\u23F5";
            BTN_Stop.Text = "\u23F9";
            LBL_SongPercent.Text = "";

            Dictionary<string, string> tracks = new Dictionary<string, string>();

            Directory.GetFiles(trackPath).ToList().ForEach(x =>
            {
                tracks.Add(Path.GetFileName(x), x);
            });

            LSB_Tracks.DataSource = tracks.ToList();
            LSB_Tracks.DisplayMember = "key";
        }
        private void BTN_Load_Click(object sender, EventArgs e)
        {
            if (this.player!=null)
            {
                this.player.Dispose();
            }

            // # Possible init 1 (easiest)
            this.player = new Player(((KeyValuePair<string, string>)LSB_Tracks.SelectedItem).Value);

            // # Possible init 2
            //this.player = new Player();
            //this.player.Load(((KeyValuePair<string, string>)LSB_Tracks.SelectedItem).Value);

            // # Possible init 3
            //this.player = new Player();
            //this.player.Filename = ((KeyValuePair<string, string>)LSB_Tracks.SelectedItem).Value;
            //this.player.Load();

            CMB_OutputDevice.DataSource = Player.GetDevices().ToList();
            CMB_OutputDevice.DisplayMember = "value";

            ToggleButtons(true);
        }
        bool togglePause;
        private void BTN_Pause_Click(object sender, EventArgs e)
        {
            togglePause = this.player.IsPlaying;
            togglePause ^= true;
            switch (togglePause)
            {
                case true:
                    this.player.Play();
                    break;
                case false:
                    this.player.Pause();
                    break;
            }
        }
        private void BTN_Stop_Click(object sender, EventArgs e)
        {
            this.player.Stop();
            SongTimer.Stop();
            PRG_SongProgress.Value = 0;
            LBL_SongPercent.Text = "";
        }
        private void BTN_Play_Click(object sender, EventArgs e)
        {
            this.player.Play();
            SongTimer.Start();
        }
        private void ToggleButtons(bool activate = false)
        {
            if (activate)
            {
                BTN_Play.Enabled = true;
                BTN_Pause.Enabled = true;
                BTN_Stop.Enabled = true;
                CMB_OutputDevice.Enabled = true;
                TRK_Volume.Enabled = true;
                return;
            }
            BTN_Play.Enabled ^= true;
            BTN_Pause.Enabled ^= true;
            BTN_Stop.Enabled ^= true;
            CMB_OutputDevice.Enabled ^= true;
            TRK_Volume.Enabled ^= true;
        }
        private void CMB_OutputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.player.OutputDevice = ((KeyValuePair<int, string>)((ComboBox)sender).SelectedItem).Key;
        }
        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            this.player.Volume = (short)((TrackBar)sender).Value;
        }
        private void SongTimer_Tick(object sender, EventArgs e)
        {
            PRG_SongProgress.Value = (int)this.player.Progress;
            LBL_SongPercent.Text = this.player.ProgressPercent;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player != null)
            {
                player.Dispose();
            }
        }
    }
}
