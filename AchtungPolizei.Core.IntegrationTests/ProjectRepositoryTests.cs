using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AchtungPolizei.Core.Helpers;
using AchtungPolizei.Plugins;
using AchtungPolizei.Plugins.Impl;
using NUnit.Framework;

namespace AchtungPolizei.Core.IntegrationTests
{
    class FooConfig : ConfigurationBase
    {
        public string Name
        {
            get { return GetParameter("key", "Default"); }
            set { parameters["key"] = value; }
        }
    }

    [TestFixture]
    public class ProjectRepositoryTests
    {
        [Test]
        public void SaveGetTest()
        {
            var repository = new ProjectsRepository();
            repository.SaveProjects(
                new[]
                    {
                        new Project
                            {
                                InputPlugin =
                                    new PluginConfiguration
                                        {
                                            PluginId = Guid.NewGuid(),
                                            Configuration =
                                                new FooConfig
                                                    {
                                                        Name = "Name 1"
                                                    }
                                        },
                                OutputPlugins =
                                    new List<PluginConfiguration>
                                        {
                                            new PluginConfiguration
                                                {
                                                    PluginId = Guid.NewGuid(),
                                                    Configuration =
                                                        new FooConfig
                                                            {
                                                                Name =
                                                                    "Name 2"
                                                            }
                                                }
                                        }
                            }
                    });

            repository = new ProjectsRepository();
            var actual = repository.GetProjects().ToArray();

            Assert.AreEqual(1, actual.Length);

            var configuration = actual[0].InputPlugin.Configuration as FooConfig;
            Assert.AreEqual("Name 1", configuration.Name);

            configuration = actual[0].OutputPlugins[0].Configuration as FooConfig;
            Assert.AreEqual("Name 2", configuration.Name);
        }
    }

    [TestFixture]
    public class OutputQueTests
    {
        [Test]
        public void ShouldBeAbleToOutput()
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

            PluginLocator.Initialize(@"D:\Develop\hackaton\AchtungPolizei\AchtungPolizei.Tray\bin\Debug\Plugins\");

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
