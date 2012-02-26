using System;
using System.Collections.Generic;
using Altalerta.Core.Essential;
using Altalerta.Core.Plugins;
using Autofac;

namespace Altalerta.Core.Tools.Impl
{
    public class PluginRepository : IPluginRepository
    {
        private readonly IDictionary<string, Type> map = new Dictionary<string, Type>();
        private IContainer container;

        #region IPluginRepository Members

        public void Discover(IContainer container)
        {
            this.container = container;
            foreach (IPlugin plugin in container.Resolve<IEnumerable<IPlugin>>())
            {
                if (!map.ContainsKey(plugin.Name))
                {
                    map.Add(plugin.Name, plugin.GetType());
                }
            }
        }

        public IPlugin Instantiate(PluginReference reference)
        {
            var plugin = container.Resolve(map[reference.Name]) as IPlugin;
            plugin.Configuration = reference.Configuration;
            return plugin;
        }

        #endregion
    }
}