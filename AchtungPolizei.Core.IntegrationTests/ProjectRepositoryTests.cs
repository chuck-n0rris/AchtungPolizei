using System;
using System.Collections.Generic;
using System.Linq;
using AchtungPolizei.Plugins;
using NUnit.Framework;

namespace AchtungPolizei.Core.IntegrationTests
{
    class FooConfig : ConfigurationBase
    {
        public string Name
        {
            get { return GetParameter("key", "Default"); }
            set { Parameters["key"] = value; }
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

            var configuration = actual[0].InputPlugin.Configuration;
            Assert.AreEqual("Name 1", configuration.Parameters["key"]);

            configuration = actual[0].OutputPlugins[0].Configuration;
            Assert.AreEqual("Name 2", configuration.Parameters["key"]);
        }
    }
}
