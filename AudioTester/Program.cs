using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using AchtungPolizei.Plugins;
using AchtungPolizei.Plugins.Impl;

namespace AudioTester
{
    class Program
    {
        private static BlockingCollection<int> que = new BlockingCollection<int>();

        private static bool shutdown = false;

        static void Main()
        {
            // TestLights();

            // TestSound();

            Task.Factory.StartNew(Consumer);

            while (Console.ReadLine() != "X")
            {
                que.TryAdd(new Random().Next(1000));
            }

            shutdown = true;

        }

        private static void Consumer()
        {
            while (!shutdown)
            {
                int result = 0;

                long bla;

                if (que.TryTake(out result, TimeSpan.FromMinutes(1) ))
                {
                    for (long i = 0; i < 100000 * result; i++)
                    {
                        bla = i + 1;
                    }

                    Console.WriteLine("Done");
                }
            }
        }



        //private static void TestLights()
        //{
        //    var lightOutputPlugin = new LightOutputPlugin();
        //    Task start = lightOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
        //    start.ContinueWith(it => Console.WriteLine("Done"));
        //    Console.ReadLine();
        //}

        //private static void TestSound()
        //{
        //    var soundOutputPlugin = new SoundOutputPlugin("aircraft012.mp3");
        //    Task task = soundOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
        //    task.ContinueWith(it => Console.WriteLine("Hello"));
        //    Console.ReadLine();
        //}
    }
}
