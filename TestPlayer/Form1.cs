using System;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using neXn.MOD;

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

            Directory.GetFiles(trackPath).ToList().ForEach(x => {
                tracks.Add(Path.GetFileName(x),x);
            });

            LSB_Tracks.DataSource = tracks.ToList();
            LSB_Tracks.DisplayMember = "key";
        }

        private void BTN_Load_Click(object sender, EventArgs e)
        {
            player = new Player(((KeyValuePair<string,string>)LSB_Tracks.SelectedItem).Value);

            CMB_OutputDevice.DataSource = player.GetDevices().ToList();
            CMB_OutputDevice.DisplayMember = "value";
        }

        bool togglePause;
        private void BTN_Pause_Click(object sender, EventArgs e)
        {
            togglePause = player.IsPlaying;
            togglePause ^= true;
            switch (togglePause)
            {
                case true:
                    player.Play();
                    break;
                case false:
                    player.Pause();
                    break;
            }
        }

        private void BTN_Stop_Click(object sender, EventArgs e)
        {
            player.Stop();
            SongTimer.Stop();
            PRG_SongProgress.Value = 0;
            LBL_SongPercent.Text = "";
        }

        private void BTN_Play_Click(object sender, EventArgs e)
        {
            player.Play();
            SongTimer.Start();
        }

        

        private void CMB_OutputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.OutputDevice = ((KeyValuePair<int, string>)((ComboBox)sender).SelectedItem).Key;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            player.Volume = (short)((TrackBar)sender).Value;
        }

        private void SongTimer_Tick(object sender, EventArgs e)
        {
            PRG_SongProgress.Value = (int)player.Progress;
            LBL_SongPercent.Text = player.ProgressPercent;
        }
    }
}
