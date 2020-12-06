namespace NUnitTests
{
    public class TestFile
    {
        public string Extension { get { return System.IO.Path.GetExtension(this.Path).Remove(0, 1); } }
        public string Path { get; set; }
        public string FullPath { get; set; }
    }
}
