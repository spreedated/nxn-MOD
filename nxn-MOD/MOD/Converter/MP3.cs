using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neXn.MOD.Converter
{
    /// <summary>
    /// Class for converting MOD files to MP3 files
    /// </summary>
    public class MP3 : Converter
    {

        #region Constructor
        private void InitConstructor()
        {

        }
        public MP3()
        {
            InitConstructor();
        }
        #endregion
        public void Convert()
        {
            //using (WaveFileReader reader = new WaveFileReader(@"C:\Users\SpReeD\Desktop\Razor1911\export\test.wav"))
            //{
            //    int numChannels = reader.WaveFormat.Channels;
            //    int sampleRate = reader.WaveFormat.SampleRate;
            //    int bitPerSample = reader.WaveFormat.BitsPerSample;

            //    byte[] buffer = new byte[65536];
                
            //    using (Stream streamWriter = File.Open(@"C:\Users\SpReeD\Desktop\Razor1911\export\export.mp3", FileMode.CreateNew))
            //    {
            //        using (LameMP3FileWriter k = new LameMP3FileWriter(streamWriter, new WaveFormat(sampleRate, 16, numChannels), 128))
            //        {
            //            reader.CopyTo(k);
            //            k.Close();
            //        }
            //        reader.Close();
            //    }
            //}


        }
    }
}
