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

        public PluginLocator(string pluginDirectoryPath)
        {
            this.pluginDirectoryPath = pluginDirectoryPath;
        }

        /// <summary>
        /// Finds plugin-compatible types in all assemblies from plugins directory.
        /// </summary>
        public IEnumerable<Type> FindPluginTypes()
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
        public IPlugin CreatePluginInstance(Type pluginType)
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