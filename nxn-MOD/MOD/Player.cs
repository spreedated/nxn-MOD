using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMik;
using SharpMik.Player;
using SharpMik.Drivers;
using neXn.MOD.Properties;

namespace neXn.MOD
{
    public class Player : IDisposable
    {
        /// <summary>
        /// Filename or Filepath to load
        /// </summary>
        public string Filename { get; set; }
        public int Position { get; set; }
        /// <summary>
        /// In Percent
        /// </summary>
        public double Progress { get; set; }
        /// <summary>
        /// Returns if playing
        /// </summary>
        public bool IsPlaying { get { return this.mikMod.IsPlaying(); } }
        /// <summary>
        /// Returns string formatted in #.00%
        /// </summary>
        public string ProgressPercent { get { return $"{this.Progress.ToString("#.00", System.Globalization.CultureInfo.InvariantCulture)}%"; } }
        public ModuleProperties ModuleProperties { get; private set; }

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

            module.volume = 64;
        }
        public void Load()
        {
            module = mikMod.LoadModule(this.Filename);
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
            mikMod.Stop();
        }

        private void PlayerChange(ModPlayer.PlayerState state)
        {
            this.Progress = Math.Round(this.mikMod.GetProgress()*100,2);
            Console.WriteLine(module.tracks);
            Console.WriteLine(module.sngremainder);
            Console.WriteLine(module.patterns);
            Console.WriteLine(module.numvoices);
            Console.WriteLine(module.numpos);
            Console.WriteLine(module.instruments);
            Console.WriteLine(module.flags);
        }

        public void Dispose()
        {
            if (mikMod!=null)
            {
                this.UnLoad();
            }
            mikMod = null;
            module = null;
        }
    }
}
