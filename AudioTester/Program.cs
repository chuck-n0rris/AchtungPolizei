using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AchtungPolizei.Plugins;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioTester
{
   

    class Program
    {
        static void Main()
        {
            TestLights();

            // TestSound();
        }

        private static void TestLights()
        {
            var lightOutputPlugin = new LightOutputPlugin();
            Task start = lightOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
            start.ContinueWith(it => Console.WriteLine("Done"));
            Console.ReadLine();
        }

        private static void TestSound()
        {
            var soundOutputPlugin = new SoundOutputPlugin("aircraft012.mp3");
            Task task = soundOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
            task.ContinueWith(it => Console.WriteLine("Hello"));
            Console.ReadLine();
        }
    }
}
