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
}