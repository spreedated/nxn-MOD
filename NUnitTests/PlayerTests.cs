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
using neXn.MOD;
using neXn.MOD.Properties;

namespace NUnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        private readonly TestFile[] testFiles = new TestFile[] {
            new TestFile()
            {
                Path = @"TestFiles\Q3A.s3m"
            },
            new TestFile()
            {
                Path = @"TestFiles\AVP.mod"
            },
            new TestFile()
            {
                Path = @"TestFiles\BF2142.xm"
            }
        };

        [SetUp]
        public void Init()
        {
            // Test if testfiles exists
            testFiles.ToList().ForEach(x=> {
                string fPath = Path.GetFullPath(Path.Combine(TestHelper.GetTestsPath(), @"..\..", x.Path));
                if (!File.Exists(fPath))
                {
                    throw new FileNotFoundException($"{x.Extension} testfile could not be found in {x.Path}");
                }
                x.FullPath = fPath;
            });
        }

        [Test]
        public async Task PlayingTests()
        {
            foreach (var item in testFiles)
            {
                await Playing(item);
            }
        }

        public async Task Playing(TestFile tF)
        {
            Player player = new Player(tF.FullPath);

            Assert.IsFalse(player.IsPlaying);
            Assert.AreEqual(0, player.Position);
            Assert.AreEqual(0, player.Progress);
            Assert.AreEqual("0.00%", player.ProgressPercent);
            Assert.IsNotNull(player.ModuleProperties);
            Assert.AreEqual(100, player.Volume);

            Volume(player, 16);
            player.Play();
            Assert.IsTrue(player.IsPlaying);

            await WaitPercent(player, 2);
            Volume(player, 24);
            await WaitPercent(player, 4);
            Volume(player, 5);
            await WaitPercent(player, 5);

            player.TogglePause();
            double pos = player.Progress;
            Assert.IsFalse(player.IsPlaying);

            await WaitMS(2000);

            player.TogglePause();
            Assert.AreEqual(pos,player.Progress);
            Assert.IsTrue(player.IsPlaying);

            await WaitPercent(player, 6);

            Assert.AreNotEqual(pos, player.Progress);

            player.Dispose();

            Console.WriteLine($"Test for \"{tF.Extension}\" completed.");
        }

        private void Volume(neXn.MOD.Player player, int vol)
        {
            player.SetVolume((short)vol);
            Assert.AreEqual(vol,player.Volume);
        }
        private async Task WaitPercent(Player player, int percent)
        {
            await Task.Factory.StartNew(() => {
                while (player.Progress < percent) { }
            });
        }
        private async Task WaitMS(int ms)
        {
            await Task.Factory.StartNew(() => {
                Thread.Sleep(ms);
            });
        }
    }
}
