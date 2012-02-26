using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AchtungPolizei.Core;
using AchtungPolizei.Core.Helpers;
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

            ShouldBeAbleToOutput();

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



        public static void ShouldBeAbleToOutput()
        {
            var outputQue = new OutputQue();

            var project = new Project()
            {
                Name = "Hello",
                OutputPlugins = new List<PluginConfiguration>
                                                      {
                                                          new PluginConfiguration()
                                                              {
                                                                  PluginId = Guid.Parse("282356A9-3E91-4405-B54B-072709E1DA09"),
                                                                  Configuration = new SoundPluginConfiguration
                                                                                      {
                                                                                         Broken = @"D:\Develop\hackaton\AchtungPolizei\AudioTester\bin\Debug\aircraft012.mp3"
                                                                                      }
                                                              }
                                                      }
            };

            PluginLocator.Initialize(@"D:\Develop\hackaton\AchtungPolizei\AudioTester\bin\Debug");

            outputQue.Start();

            outputQue.Add(new ProcessRequest
            {
                Project = project,
                ProjectState = new ProjectState
                {
                    BuildState = new BuildState(),
                    BuildStatus = BuildStatus.Broken

                }
            });

            Thread.Sleep(100000);
        }
    }
}
