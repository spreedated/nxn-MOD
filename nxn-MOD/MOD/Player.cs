using neXn.MOD.Properties;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
using System;
using System.Diagnostics;
using System.Collections.Generic;
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
        #region Module Properties To Change
        /// <summary>
        /// Set Volume from 0-100
        /// </summary>
        public short Volume 
        {
            set { this.module.volume = (short)HelperFunctions.Map(value, 0, 100, 0, 128); }
            get { return (short)HelperFunctions.Map(this.module.volume, 0, 128, 0, 100); }
        }
        /// <summary>
        /// Select outputdevice, get a list of devices through "GetDevices()"<br/>
        /// Note: Changing device while playing will reload the module.
        /// </summary>
        public int OutputDevice
        {
            set { 
                _NAudioOptions.OutputDevice = value; 
                bool isCurrentlyPlaying = this.IsPlaying;
                this.ReLoad(); 
                if (isCurrentlyPlaying) 
                {
                    this.Play();
                } 
            }
            get { return _NAudioOptions.OutputDevice; }
        }
        /// <summary>
        /// Replay module after module end
        /// </summary>
        public bool Loop
        {
            set { this.module.loop = value; }
            get { return this.module.loop; }
        }
        #endregion
        private NaudioDriverAdvanced.NaudioDriverAdvacedOptions _NAudioOptions = new NaudioDriverAdvanced.NaudioDriverAdvacedOptions();
        private MikMod mikMod;
        private Module module;

        #region Constructor
        private void InitConstructor(string fileName, Action action)
        {
            this.Filename = fileName;
            mikMod = new MikMod();
            action();

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
        public Player(string fileName)
        {
            InitConstructor(fileName, () => { mikMod.Init<NaudioDriverAdvanced>(_NAudioOptions); });
        }
        public Player(string fileName, int outputDevice)
        {
            _NAudioOptions.OutputDevice = outputDevice;
            InitConstructor(fileName, () => { mikMod.Init<NaudioDriverAdvanced>(_NAudioOptions); });
        }
        #endregion
        //TODO: gtg
        public void Load(string modulePath = this.Filename)
        {
            if (this.module==null)
            {
                this.module = mikMod.LoadModule(this.Filename);
            }
            else
            {
                throw new Exception($"There's already a module loaded, unload first, loaded module \"{this.Filename}\"");
            }
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
            Debug.Print($"[nxn-MOD] Module loaded --{this.module.songname}--\n\t\t  File -- {Path.GetFileName(this.Filename)}");
        }
        public void UnLoad()
        {
            if (this.mikMod.IsPlaying())
            {
                this.Stop();
            }
            if (this.mikMod != null)
            {
                mikMod.UnLoadModule(module);
            }
        }
        public void ReLoad()
        {
            this.UnLoad();
            this.Load();
        }
        public void Play()
        {
            mikMod.Play(module);
            Debug.Print($"[nxn-MOD] Playing --{this.module.songname}--\n\t\t  On device -- {this.GetDevices()[this.OutputDevice]}");
        }
        public void Stop()
        {
            mikMod.SetPosition(0);
            mikMod.Stop();
        }
        public void Pause()
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
        /// <summary>
        /// Output a Dictionary of int,string<br/>
        /// identifier,productName
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,string> GetDevices()
        {
            return NaudioDriverAdvanced.NaudioDriverAdvacedOptions.GetOutputDevices();
        }

        private void PlayerChange(ModPlayer.PlayerState state)
        {
            if (this.mikMod == null)
            {
                return;
            }
            this.Progress = Math.Round(this.mikMod.GetProgress() * 100, 2);

            // Loop function
            if (this.Loop && !this.IsPlaying && this.Progress == 0)
            {
                this.Stop();
                this.Play();
            }
            //# ### #
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
