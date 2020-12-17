using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using neXn.MOD.Converter;
using ByteSizeLib;

namespace NUnitTests
{
    [TestFixture]
    public class ConverterTests_WAVE_MP3
    {
            private readonly string testFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", string.Empty), @"..\..\..\NUnitTests\TestFiles"));
            private string testFile;

            [SetUp]
            public void SetUp()
            {
                this.testFile = Path.Combine(testFilePath, "testOut.wav");
                //DeleteTestFile();
            }
            [Test]
            public void DontOverwriteFile()
            {
                //using (Wave k = new Wave() { OverwriteExisting = false, OutputFilepath = Path.Combine(testFilePath, "AVP.mod") })
                //{
                //    Assert.Throws<Exception>(() => { k.Convert(); });
                //}
            }
            [Test]
            public void ConverstionTest()
            {
                using (MP3 k = new MP3())
                {
                    k.Convert();
                }

                //Assert.IsTrue(File.Exists(this.testFile));

                //FileInfo acc = new FileInfo(this.testFile);
                //Console.WriteLine($"Filesize of conversion is {ByteSize.FromBytes(acc.Length).MegaBytes:0.##} MB ({ByteSize.FromBytes(acc.Length).MebiBytes:0.##} MiB)");

                //Assert.Greater(acc.Length, 1024);
                //Assert.AreEqual(".wav", acc.Extension);
            }

            [TearDown]
            public void TearDown()
            {
                //DeleteTestFile();
            }

            private void DeleteTestFile()
            {
                if (File.Exists(this.testFile))
                {
                    File.Delete(this.testFile);
                }
            }
    }
}
