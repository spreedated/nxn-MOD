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
            base.OutputFileCheck();

            WavDriver.WavDriverOptions e = new WavDriver.WavDriverOptions
            {
                OutputFilename = this.OutputFilepath,
                Overwrite = base.OverwriteExisting
            };

            base.mikMod.Init<WavDriver>(e);
            //if (!base.mikMod.Init<WavDriver>(e))
            //{
            //    throw new Exception($"Error loading Driver");
            //}

            base.module = base.mikMod.LoadModule(base.InputFilename);

            if (base.module==null)
            {
                throw new Exception($"Error loading Module");
            }
            
            base.module.loop = false;

            Conversion();
        }

        private void Conversion()
        {
            base.mikMod.Play(base.module); //Must play to convert...
            base.mikMod.Update(); //First step
            while (base.mikMod.GetProgress() > 0f)
            {
                base.mikMod.Update();
            }
            base.mikMod.Exit(); //Write header files (finish)
        }
    }
}
