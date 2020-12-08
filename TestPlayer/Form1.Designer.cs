
namespace TestPlayer
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
            this.BTN_Play = new System.Windows.Forms.Button();
            this.BTN_Stop = new System.Windows.Forms.Button();
            this.BTN_Load = new System.Windows.Forms.Button();
            this.BTN_Pause = new System.Windows.Forms.Button();
            this.CMB_OutputDevice = new System.Windows.Forms.ComboBox();
            this.TRK_Volume = new System.Windows.Forms.TrackBar();
            this.PRG_SongProgress = new System.Windows.Forms.ProgressBar();
            this.SongTimer = new System.Windows.Forms.Timer(this.components);
            this.LBL_SongPercent = new System.Windows.Forms.Label();
            this.LSB_Tracks = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.TRK_Volume)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_Play
            // 
            this.BTN_Play.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Play.Location = new System.Drawing.Point(216, 76);
            this.BTN_Play.Name = "BTN_Play";
            this.BTN_Play.Size = new System.Drawing.Size(26, 26);
            this.BTN_Play.TabIndex = 0;
            this.BTN_Play.Text = "Play";
            this.BTN_Play.UseVisualStyleBackColor = true;
            this.BTN_Play.Click += new System.EventHandler(this.BTN_Play_Click);
            // 
            // BTN_Stop
            // 
            this.BTN_Stop.Location = new System.Drawing.Point(216, 12);
            this.BTN_Stop.Name = "BTN_Stop";
            this.BTN_Stop.Size = new System.Drawing.Size(26, 26);
            this.BTN_Stop.TabIndex = 1;
            this.BTN_Stop.Text = "Stop";
            this.BTN_Stop.UseVisualStyleBackColor = true;
            this.BTN_Stop.Click += new System.EventHandler(this.BTN_Stop_Click);
            // 
            // BTN_Load
            // 
            this.BTN_Load.Location = new System.Drawing.Point(184, 12);
            this.BTN_Load.Name = "BTN_Load";
            this.BTN_Load.Size = new System.Drawing.Size(26, 26);
            this.BTN_Load.TabIndex = 2;
            this.BTN_Load.Text = "Load module";
            this.BTN_Load.UseVisualStyleBackColor = true;
            this.BTN_Load.Click += new System.EventHandler(this.BTN_Load_Click);
            // 
            // BTN_Pause
            // 
            this.BTN_Pause.Location = new System.Drawing.Point(216, 44);
            this.BTN_Pause.Name = "BTN_Pause";
            this.BTN_Pause.Size = new System.Drawing.Size(26, 26);
            this.BTN_Pause.TabIndex = 3;
            this.BTN_Pause.Text = "Pause";
            this.BTN_Pause.UseVisualStyleBackColor = true;
            this.BTN_Pause.Click += new System.EventHandler(this.BTN_Pause_Click);
            // 
            // CMB_OutputDevice
            // 
            this.CMB_OutputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMB_OutputDevice.FormattingEnabled = true;
            this.CMB_OutputDevice.Location = new System.Drawing.Point(263, 12);
            this.CMB_OutputDevice.Name = "CMB_OutputDevice";
            this.CMB_OutputDevice.Size = new System.Drawing.Size(166, 21);
            this.CMB_OutputDevice.TabIndex = 4;
            this.CMB_OutputDevice.SelectedIndexChanged += new System.EventHandler(this.CMB_OutputDevice_SelectedIndexChanged);
            // 
            // TRK_Volume
            // 
            this.TRK_Volume.Location = new System.Drawing.Point(263, 75);
            this.TRK_Volume.Maximum = 100;
            this.TRK_Volume.Name = "TRK_Volume";
            this.TRK_Volume.Size = new System.Drawing.Size(166, 45);
            this.TRK_Volume.TabIndex = 5;
            this.TRK_Volume.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // PRG_SongProgress
            // 
            this.PRG_SongProgress.Location = new System.Drawing.Point(11, 139);
            this.PRG_SongProgress.Name = "PRG_SongProgress";
            this.PRG_SongProgress.Size = new System.Drawing.Size(418, 23);
            this.PRG_SongProgress.TabIndex = 6;
            // 
            // SongTimer
            // 
            this.SongTimer.Interval = 250;
            this.SongTimer.Tick += new System.EventHandler(this.SongTimer_Tick);
            // 
            // LBL_SongPercent
            // 
            this.LBL_SongPercent.AutoSize = true;
            this.LBL_SongPercent.BackColor = System.Drawing.Color.Transparent;
            this.LBL_SongPercent.Location = new System.Drawing.Point(11, 123);
            this.LBL_SongPercent.Name = "LBL_SongPercent";
            this.LBL_SongPercent.Size = new System.Drawing.Size(85, 13);
            this.LBL_SongPercent.TabIndex = 7;
            this.LBL_SongPercent.Text = "###percent###";
            // 
            // LSB_Tracks
            // 
            this.LSB_Tracks.FormattingEnabled = true;
            this.LSB_Tracks.Location = new System.Drawing.Point(11, 12);
            this.LSB_Tracks.Name = "LSB_Tracks";
            this.LSB_Tracks.Size = new System.Drawing.Size(167, 108);
            this.LSB_Tracks.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 185);
            this.Controls.Add(this.LSB_Tracks);
            this.Controls.Add(this.LBL_SongPercent);
            this.Controls.Add(this.PRG_SongProgress);
            this.Controls.Add(this.TRK_Volume);
            this.Controls.Add(this.CMB_OutputDevice);
            this.Controls.Add(this.BTN_Pause);
            this.Controls.Add(this.BTN_Load);
            this.Controls.Add(this.BTN_Stop);
            this.Controls.Add(this.BTN_Play);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TRK_Volume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Play;
        private System.Windows.Forms.Button BTN_Stop;
        private System.Windows.Forms.Button BTN_Load;
        private System.Windows.Forms.Button BTN_Pause;
        private System.Windows.Forms.ComboBox CMB_OutputDevice;
        private System.Windows.Forms.TrackBar TRK_Volume;
        private System.Windows.Forms.ProgressBar PRG_SongProgress;
        private System.Windows.Forms.Timer SongTimer;
        private System.Windows.Forms.Label LBL_SongPercent;
        private System.Windows.Forms.ListBox LSB_Tracks;
    }
}

