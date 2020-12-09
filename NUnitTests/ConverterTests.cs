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
        [Test]
        public void Test()
        {
            Wave k = new Wave();
            k.OverwriteExisting = true;
            k.OutputFilepath = @"C:\Users\SpReeD\Desktop\autopilot - cockpit\32999_1280.webp";
            k.Convert();
        }
    }
}
