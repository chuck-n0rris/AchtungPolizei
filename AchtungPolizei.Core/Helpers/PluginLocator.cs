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
        private static readonly Type pluginInterface = typeof(IPlugin);
        private static readonly Type inputPluginInterface = typeof(IInputPlugin);
        private static readonly Type outputPluginInterface = typeof(IOutputPlugin);

        private PluginLocator(string pluginDirectoryPath)
        {
            this.pluginDirectoryPath = pluginDirectoryPath;
        }

        private static IEnumerable<Type> pluginTypes;
        private static Dictionary<Guid, Type> idToTypeMap;

        public IPlugin GetInstanceById(Guid pluginId)
        {
            return CreatePluginInstance(idToTypeMap[pluginId]);
        }

        private static IEnumerable<IPlugin> GetAllPlugins()
        {
            return pluginTypes
                .Select(CreatePluginInstance)
                .ToList();
        }

        public IEnumerable<IInputPlugin> GetAllInputPlugins()
        {
            return pluginTypes
                .Where(inputPluginInterface.IsAssignableFrom)
                .Select(CreatePluginInstance)
                .Cast<IInputPlugin>()
                .ToList();
        }

        public IEnumerable<IOutputPlugin> GetAllOutputPlugins()
        {
            return pluginTypes
                .Where(outputPluginInterface.IsAssignableFrom)
                .Select(CreatePluginInstance)
                .Cast<IOutputPlugin>()
                .ToList();
        }

        public static PluginLocator Current { get; private set; }

        public static void Initialize(string path)
        {
            var pluginLocator = new PluginLocator(path);
            pluginTypes = pluginLocator.FindPluginTypes();

            // Prepare the fast mapping from plugin id to plugin type.
            idToTypeMap = new Dictionary<Guid, Type>();
            var dummyInstances = GetAllPlugins();
            foreach (var dummyInstance in dummyInstances)
            {
                idToTypeMap.Add(dummyInstance.Id, dummyInstance.GetType());
                dummyInstance.Dispose();
            }

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
        private static IPlugin CreatePluginInstance(Type pluginType)
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