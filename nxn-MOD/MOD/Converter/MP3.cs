using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NAudio.Lame;
//using NAudio.Wave;

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
            //using (WaveFileReader reader = new WaveFileReader("temp.wav"))
            //{
            //    int numChannels = reader.WaveFormat.Channels;
            //    int sampleRate = reader.WaveFormat.SampleRate;
            //    int bitPerSample = reader.WaveFormat.BitsPerSample;

            //    //LameMP3FileWriter k = new LameMP3FileWriter("", new WaveFormat(sampleRate, 16, numChannels), 128);

            //}


        }
        #endregion
    }
}
