using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AchtungPolizei.Core;
using AchtungPolizei.Core.Helpers;
using AchtungPolizei.Plugins;
using AchtungPolizei.Plugins.Impl;
using AchtungPolizei.Plugins.TextToSpeech;

namespace AudioTester
{
    class Program
    {
        private static BlockingCollection<int> que = new BlockingCollection<int>();

        private static bool shutdown = false;

        static void Main()
        {
          ShouldBeAbleToOutput();
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
                                                                                         Broken = @"D:\chimes.wav",
                                                                                         Fixed = @"D:\chimes.wav",
                                                                                      }
                                                              },
                                                              
                                                              //new PluginConfiguration()
                                                              //{
                                                              //    PluginId = Guid.Parse("7C9641FC-A6DE-4854-B583-CCD559C6C037"),
                                                              //    Configuration = new LightPluginConfiguration()
                                                              //                        {
                                                              //                            Device = "Device2",
                                                              //                            Miliseconds = 15000,
                                                              //                            Path = @"C:\Program Files\Gembird\Power Manager\pm.exe",
                                                              //                            Socket = "Socket1"
                                                              //                        }
                                                              //},

                                                              //new PluginConfiguration()
                                                              //{
                                                              //    PluginId = Guid.Parse("603e7da9-4cb1-4ac7-b84e-7ce12b3cbee3"),
                                                              //    Configuration = new TextToSpeechConfiguration()
                                                              //                        {
                                                              //                            BuildBrokenPhrase = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                                                              //                            BuildFixedPhrase = "I want to grape you in the mouth"
                                                              //                        }
                                                              //}
                                                      }
            };

            PluginLocator.Initialize(@".");

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

            outputQue.Add(new ProcessRequest
            {
                Project = project,
                ProjectState = new ProjectState
                {
                    BuildState = new BuildState(),
                    BuildStatus = BuildStatus.Fixed
                }
            });

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
