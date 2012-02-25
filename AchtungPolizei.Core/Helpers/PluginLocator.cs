namespace AchtungPolizei.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    
    using AchtungPolizei.Plugins;

    public class PluginLocator
    {
        private readonly string pluginDirectoryPath;
        private readonly Type pluginInterface = typeof(IPlugin);

        private PluginLocator(string pluginDirectoryPath)
        {
            this.pluginDirectoryPath = pluginDirectoryPath;
        }

        public IEnumerable<IInputPlugin> InputPlugins { get; private set; }

        public IEnumerable<IOutputPlugin> OutputPlugins { get; private set; }

        public static PluginLocator Current { get; private set; }

        public static void Initialize(string path)
        {
            var pluginLocator = new PluginLocator(path);
            var pluginTypes = pluginLocator.FindPluginTypes();

            var inputPlugins = new List<IInputPlugin>();
            var outputPugins = new List<IOutputPlugin>();

            foreach (var pluginType in pluginTypes)
            {
                var pluginInstance = pluginLocator.CreatePluginInstance(pluginType);

                if (pluginInstance is IInputPlugin)
                {
                    inputPlugins.Add((IInputPlugin)pluginInstance);
                }

                if (pluginInstance is IOutputPlugin)
                {
                    outputPugins.Add((IOutputPlugin)pluginInstance);
                }
            }

            pluginLocator.InputPlugins = inputPlugins;
            pluginLocator.OutputPlugins = outputPugins;

            Current = pluginLocator;
        }

        /// <summary>
        /// Finds plugin-compatible types in all assemblies from plugins directory.
        /// </summary>
        private IEnumerable<Type> FindPluginTypes()
        {
            var pluginDirectory = new DirectoryInfo(pluginDirectoryPath);

            var pluginTypes = new List<Type>();
            foreach (var file in pluginDirectory.EnumerateFiles("*.dll"))
            {
                try
                {
                    Assembly pluginAssembly = Assembly.LoadFile(file.FullName);
                    pluginTypes.AddRange(FindPluginTypesInAssembly(pluginAssembly));
                }
                catch (FileLoadException)
                {
                }
                catch (BadImageFormatException)
                {
                }
            }

            return pluginTypes;
        }

        /// <summary>
        /// Creates plugin instance by plugin type.
        /// </summary>
        /// <param name="pluginType"> Plugin type. </param>
        /// <returns> Plugin instance. </returns>
        private IPlugin CreatePluginInstance(Type pluginType)
        {
            if (!pluginInterface.IsAssignableFrom(pluginType) || !pluginType.IsClass)
            {
                throw new ArgumentException("Plugin type is not a class or doesn't implement IPlugin interface");
            }

            ConstructorInfo ci = pluginType.GetConstructor(new Type[] { });
            return (IPlugin)ci.Invoke(new Object[] { });
        }   
        
        private IEnumerable<Type> FindPluginTypesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(pluginInterface.IsAssignableFrom)
                .ToList();
        }
    }
}