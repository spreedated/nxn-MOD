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
    /// <summary>
    /// Class for playing module files
    /// </summary>
    public class Player : IDisposable
    {
        /// <summary>
        /// Filename or Filepath to load
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Current song position
        /// </summary>
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
        /// <summary>
        /// If a module is loaded, one can access its properties
        /// </summary>
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
        private readonly NaudioDriverAdvanced.NaudioDriverAdvacedOptions _NAudioOptions = new NaudioDriverAdvanced.NaudioDriverAdvacedOptions();
        private MikMod mikMod;
        private Module module;

        #region Constructor
        private void InitConstructor(string fileName, bool autoLoad = true)
        {
            this.Filename = fileName;
            this.mikMod = new MikMod();
            this.mikMod.Init<NaudioDriverAdvanced>(_NAudioOptions);
            this.mikMod.PlayerStateChangeEvent += new ModPlayer.PlayerStateChangedEvent(PlayerChange);
            if (autoLoad)
            {
                this.Load();
            }
        }
        /// <summary>
        /// Plain constructor<br/>
        /// need to manually load a module
        /// </summary>
        public Player()
        {
            InitConstructor(null, false);
        }
        /// <summary>
        /// Autoloads provided file as module
        /// </summary>
        /// <param name="fileName">Module filepath</param>
        public Player(string fileName)
        {
            InitConstructor(fileName);
        }
        /// <summary>
        /// Autoloads provided file as module
        /// </summary>
        /// <param name="fileName">Module filepath</param>
        /// <param name="outputDevice">Device-Identifier, get a list from GetDevices()</param>
        public Player(string fileName, int outputDevice)
        {
            _NAudioOptions.OutputDevice = outputDevice;
            InitConstructor(fileName);
        }
        #endregion

        /// <summary>
        /// Load a module file<br/>
        /// for reload use Reload()<br/>
        /// for load a new module, use Unload() first<br/>
        /// </summary>
        /// <param name="modulePath">Path to the module. null value will try to use property Filename</param>
        public void Load(string modulePath = null)
        {
            if (this.IsPlaying)
            {
                this.Stop();
            }
            if (this.module != null)
            {
                throw new Exception($"There's already a module loaded, unload first, loaded module \"{modulePath}\"");
            }
            if (modulePath!=null || this.Filename!=null)
            {
                modulePath = modulePath??this.Filename;
                if (!File.Exists(modulePath))
                {
                    throw new FileNotFoundException();
                }
                this.module = mikMod.LoadModule(modulePath);
            }
            else
            {
                throw new Exception("No module provided");
            }
            if (this.module == null)
            {
                throw new Exception($"Error loading module file \"{modulePath}\"");
            }
            LoadModuleProperties();
            Debug.Print($"[nxn-MOD] Module loaded --{this.module.songname}--\n\t\t  File -- {Path.GetFileName(modulePath)}");
        }
        private void LoadModuleProperties()
        {
            try
            {
                this.ModuleProperties = new ModuleProperties()
                {
                    Songname = this.module.songname,
                    BPM = this.module.bpm,
                    Comments = this.module.comment,
                    MODType = this.module.modtype,
                    NumberPositions = this.module.numpos,
                    NumberChannels = this.module.realchn,
                    NumberInstruments = this.module.numins,
                    NumberPatters = this.module.numpat,
                    NumberSamples = this.module.numsmp,
                    NumberTracks = this.module.numtrk,
                    NumberTotalChannels = this.module.totalchn
                };
            }
            catch (Exception ex)
            {
                Debug.Print($"[nxn-MOD] Failed to load module properties \"{ex.Message}\"");
            }
        }
        /// <summary>
        /// Unloads a loaded module/file
        /// </summary>
        public void UnLoad()
        {
            if (this.mikMod.IsPlaying())
            {
                this.Stop();
            }
            if (this.mikMod != null)
            {
                this.mikMod.UnLoadModule(module);
                this.module = null;
                this.ModuleProperties = null;
            }
        }
        /// <summary>
        /// Quick access to UnLoad() and Load()
        /// </summary>
        public void ReLoad()
        {
            this.UnLoad();
            this.Load();
        }
        /// <summary>
        /// Start playing or resume from pause
        /// </summary>
        public void Play()
        {
            if (module==null)
            {
                throw new Exception("No module loaded");
            }
            mikMod.Play(module);
            Debug.Print($"[nxn-MOD] Playing --{this.module.songname}--\n\t\t  On device -- {Player.GetDevices()[this.OutputDevice]}");
        }
        /// <summary>
        /// Stop playing
        /// </summary>
        public void Stop()
        {
            mikMod.SetPosition(0);
            mikMod.Stop();
        }
        /// <summary>
        /// Toggle pause and play
        /// </summary>
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
        /// Outputs a Dictionary of int,string<br/>
        /// identifier,productName
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int,string> GetDevices()
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
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.IsPlaying)
            {
                this.Stop();
            }
            if (this.mikMod != null)
            {
                this.UnLoad();
            }
            this.mikMod.Dispose();
        }
    }
}
