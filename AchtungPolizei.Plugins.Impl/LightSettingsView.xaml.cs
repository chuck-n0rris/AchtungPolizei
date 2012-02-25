using System;
using System.Windows.Controls;

namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Interaction logic for LightSettingsView.xaml
    /// </summary>
    public partial class LightSettingsView : UserControl, IConfigirationControl
    {
        public LightSettingsView()
        {
            InitializeComponent();
        }

        public bool Validate()
        {
            return Model.Error == null;
        }

        public ConfigurationBase GetConfiguration()
        {
            var config = new LightPluginConfiguration
                             {
                                 Device = Model.Device,
                                 Path = Model.Path,
                                 Socket = Model.Socket
                             };
            return config;
        }

        public Type GetConfigurationType()
        {
            return typeof(LightPluginConfiguration);
        }

        private LightSettingsModel Model
        {
            get
            {
                return (LightSettingsModel) DataContext;
            }
        }
    }
}
