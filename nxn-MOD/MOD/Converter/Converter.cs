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
    public abstract class Converter : IConverter, IDisposable
    {
        public bool OverwriteExisting { get; set; }
        public string OutputFilepath { get; set; }
        public string InputFilename { get; set; }

        public MikMod mikMod;
        public Module module;

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

        public void OutputFileCheck()
        {
            if (File.Exists(this.OutputFilepath))
            {
                throw new Exception($"Output file does already exist \"{Path.GetFileName(this.OutputFilepath)}\"");
            }
        }

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
