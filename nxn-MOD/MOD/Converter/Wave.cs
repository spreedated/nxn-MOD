using SharpMik.Drivers;
using SharpMik.Player;
using System;

namespace neXn.MOD.Converter
{
    /// <summary>
    /// Class for converting MOD files to Wave files
    /// </summary>
    public class Wave : Converter
    {
        private void InitContructor()
        {
            base.mikMod = new MikMod();
        }
        #region Constructor
        /// <summary>
        /// Simple constructor, you need to manually set the outputFilename
        /// </summary>
        public Wave()
        {
            InitContructor();
        }
        /// <summary>
        /// Most convenient constructor
        /// </summary>
        /// <param name="outputFilename"></param>
        public Wave(string outputFilename)
        {
            InitContructor();
            base.OutputFilepath = outputFilename;
        }
        #endregion
        /// <summary>
        /// Actual converting function
        /// </summary>
        public void Convert()
        {
            base.OverwriteCheck();
            base.OutputFileCheck();

            WavDriver.WavDriverOptions e = new WavDriver.WavDriverOptions
            {
                OutputFilename = this.OutputFilepath,
                Overwrite = base.OverwriteExisting
            };

            if (!base.mikMod.Init<WavDriver>(e))
            {
                throw new Exception($"Error loading Driver");
            }

            base.module = base.mikMod.LoadModule(base.InputFilename);

            if (base.module == null)
            {
                throw new Exception($"Error loading Module");
            }

            base.module.loop = false;

            Conversion();
        }
        /// <summary>
        /// Conversion sub-routine
        /// </summary>
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
