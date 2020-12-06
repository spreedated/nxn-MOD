using neXn.MOD.Properties;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
using System;
using System.IO;

namespace neXn.MOD
{
    public class Player : IDisposable
    {
        /// <summary>
        /// Filename or Filepath to load
        /// </summary>
        public string Filename { get; set; }
        public int Position { get { return (int)this.module.sngpos; } }
        /// <summary>
        /// In Percent
        /// </summary>
        public double Progress { get; private set; }
        /// <summary>
        /// Returns if playing
        /// </summary>
        public bool IsPlaying { get { return this.mikMod.IsPlaying(); } }
        /// <summary>
        /// Returns string formatted in #.00%
        /// </summary>
        public string ProgressPercent { get { return $"{this.Progress.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}%"; } }
        public ModuleProperties ModuleProperties { get; private set; }
        public short Volume { get { return (short)HelperFunctions.Map(this.module.volume, 0, 128, 0, 100); } }

        private MikMod mikMod;
        private Module module;
        public Player(string fileName)
        {
            mikMod = new MikMod();
            mikMod.Init<NaudioDriver>();
            this.Filename = fileName;

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            else
            {
                this.Load();
            }
            mikMod.PlayerStateChangeEvent += new ModPlayer.PlayerStateChangedEvent(PlayerChange);
        }
        /// <summary>
        /// Set Module volume <br/>
        /// module will be reloaded.
        /// </summary>
        /// <param name="percent">0-100</param>
        public void SetVolume(short percent)
        {
            this.module.volume = (short)HelperFunctions.Map(percent, 0, 100, 0, 128);
        }
        public void Load()
        {
            this.module = mikMod.LoadModule(this.Filename);
            if (this.module == null)
            {
                throw new Exception($"Error loading module file \"{this.Filename}\"");
            }
            this.ModuleProperties = new ModuleProperties()
            {
                Songname = this.module.songname,
                BPM = this.module.bpm,
                Comments = this.module.comment,
                MODType = this.module.modtype
            };
        }
        public void UnLoad()
        {
            if (this.mikMod.IsPlaying())
            {
                this.Stop();
            }
            mikMod.UnLoadModule(module);
        }
        public void Play()
        {
            mikMod.Play(module);
        }
        public void Stop()
        {
            this.Play(); //Needs to play to set position ... 
            mikMod.SetPosition(0);
            mikMod.Stop();
        }
        public void TogglePause()
        {
            switch (this.IsPlaying)
            {
                case true:
                    mikMod.Stop();
                    break;
                case false:
                    this.Play();
                    break;
            }
        }

        private void PlayerChange(ModPlayer.PlayerState state)
        {
            if (this.mikMod == null)
            {
                return;
            }
            this.Progress = Math.Round(this.mikMod.GetProgress() * 100, 2);
        }

        public void Dispose()
        {
            if (mikMod != null)
            {
                this.UnLoad();
            }
            mikMod = null;
            module = null;
        }
    }
}
