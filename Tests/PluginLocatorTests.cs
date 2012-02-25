using System.IO;
using System.Reflection;
using AchtungPolizei.Core.Helpers;
using AchtungPolizei.Plugins;

namespace Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class PluginLocatorTests
    {
        [Test]
        public void FindPluginTypes()
        {
            var assemblyFile = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string assemblyDirectory = assemblyFile.Directory.FullName;
            string pluginsDirectory = Path.Combine(assemblyDirectory, "Plugins");

            //var pluginLocator = new PluginLocator(pluginsDirectory);
            //var pluginTypes = pluginLocator.FindPluginTypes();
            //foreach (var pluginType in pluginTypes)
            //{
            //    IPlugin plugin = pluginLocator.CreatePluginInstance(pluginType);
            //}
        }
    }
}