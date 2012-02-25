namespace AchtungPolizei.Tray
{
    using System;
    using AchtungPolizei.Plugins;

    public class PluginPreviewItem : ViewModelBase
    {
        private readonly IPlugin inputPlugins;

        public PluginPreviewItem(IPlugin plugin)
        {
            this.inputPlugins = plugin;
        }

        public Guid Id
        {
            get
            {
                return this.inputPlugins.Id;
            }
        }

        public string Name
        {
            get
            {
                return this.inputPlugins.Name;
            }
        }

        public IPlugin Source { get; set; }
    }

    public class PluginViewModel : ViewModelBase
    {
        private readonly IPlugin plugin;
        private IConfigirationControl configuration;

        public PluginViewModel(IPlugin plugin)
        {
            this.plugin = plugin;
        }

        public Guid Id
        {
            get
            {
                return this.plugin.Id;
            }
        }

        public string Name
        {
            get
            {
                return this.plugin.Name;
            }
        }
        
        public IConfigirationControl Configiration
        {
            get
            {
                return this.configuration;
            }
        }
    }
}