using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;

namespace neXn.MOD.Converter
{
    /// <summary>
    /// Abstraction of Converter<br/>
    /// including all base methods
    /// </summary>
    public abstract class Converter : IConverter, IDisposable
    {
        /// <summary>
        /// Should be the file overwritten, when existing?
        /// </summary>
        public bool OverwriteExisting { get; set; }
        /// <summary>
        /// Filepath for the output
        /// </summary>
        public string OutputFilepath { get; set; }
        /// <summary>
        /// Mod file to convert
        /// </summary>
        public string InputFilename { get; set; }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public MikMod mikMod;
        public Module module;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Checks if overwrite bool is set<br/>
        /// if not it throws an exception when file exists
        /// </summary>
        public void OverwriteCheck()
        {
            if (!this.OverwriteExisting && File.Exists(this.OutputFilepath))
            {
                throw new Exception($"File ({this.OutputFilepath}) does exists and overwrite is not permitted by property \"OverwriteExisting\"");
            }
        }
        /// <summary>
        /// Checks if file already exists
        /// </summary>
        public void OutputFileCheck()
        {
            if (File.Exists(this.OutputFilepath))
            {
                throw new Exception($"Output file does already exist \"{Path.GetFileName(this.OutputFilepath)}\"");
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.mikMod!=null)
            {
                mikMod.Dispose();
            }
            this.module = null;
        }
    }

    interface IConverter
    {
        bool OverwriteExisting { get; set; }
        string OutputFilepath { get; set; }
        string InputFilename { get; set; }
        void OverwriteCheck();
    }
}
