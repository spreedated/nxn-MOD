namespace neXn.MOD.Properties
{
    /// <summary>
    /// General Module Information
    /// </summary>
    public class ModuleProperties
    {
        public ushort BPM { get; set; }
        /// <summary>
        /// Name of song
        /// </summary>
        public string Songname { get; set; }
        /// <summary>
        /// Module comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// String type of module
        /// </summary>
        public string MODType { get; set; }
        public ushort NumberPositions { get; set; }
        public ushort NumberPatters { get; set; }
        public ushort NumberInstruments { get; set; }
        public ushort NumberSamples { get; set; }
        /// <summary>
        /// Real number of channels used
        /// </summary>
        public ushort NumberChannels { get; set; }
        /// <summary>
        /// Number of total channels including NNAs
        /// </summary>
        public ushort NumberTotalChannels { get; set; }
        public ushort NumberTracks { get; set; }
        public ushort SongSpeed { get; set; }
    }
}
