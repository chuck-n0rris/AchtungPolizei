using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AchtungPolizei.Plugins;

namespace Tests
{
    [TestFixture]
    public class AudioPluginTests
    {
        [Test]
        public void TestPlugin()
        {
            var soundOutputPlugin = new SoundOutputPlugin("helli");
            var task = soundOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
            task.Wait();

        }
        
        [Test]
        public void TestPlugin2()
        {
            var soundOutputPlugin = new SoundOutputPlugin(@"C:\Users\Public\Music\Sample Music\Kalimba.mp3");
            var task = soundOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
            task.Wait();

        }
    }
}
