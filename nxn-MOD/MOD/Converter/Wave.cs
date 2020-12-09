using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpMik;
using SharpMik.Drivers;
using SharpMik.Player;
using System.Diagnostics;
using System.IO;

namespace neXn.MOD.Converter
{
    //TODO: continue
    public class Wave : Converter, IConverter
    {
        private void InitContructor()
        {
            base.mikMod = new MikMod();
        }
        #region Constructor
        public Wave()
        {
            InitContructor();
        }
        public Wave(string outputFilename)
        {
            InitContructor();
            base.OutputFilepath = outputFilename;
        }
        #endregion
        public void Convert()
        {
            base.OverwriteCheck();

            WavDriver.WavDriverOptions e = new WavDriver.WavDriverOptions();
            e.OutputFilename = this.OutputFilepath;
            e.Overwrite = base.OverwriteExisting;

            if (!base.mikMod.Init<WavDriver>(e))
            {
                throw new Exception($"Error loading Driver");
            } 


        }
    }
}
