using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using neXn.MOD.Converter;
using neXn.MOD.Properties;

namespace NUnitTests
{
    [TestFixture]
    public class ConverterTests
    {
        private readonly string testFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", string.Empty), @"..\..\..\NUnitTests\TestFiles"));

        [Test]
        public void DontOverwriteFile()
        {
            using (Wave k = new Wave() { OverwriteExisting = false, OutputFilepath = Path.Combine(testFilePath, "AVP.mod")} )
            {
                Assert.Throws<Exception>(()=> { k.Convert(); });
            }
        }
        [Test]
        public void ConverstionTest()
        {
            using (Wave k = new Wave() { OverwriteExisting = false, OutputFilepath = Path.Combine(testFilePath, "testOut.wav"), InputFilename= Path.Combine(testFilePath, "AVP.mod") })
            {
                k.Convert();
            }
        }
    }
}
