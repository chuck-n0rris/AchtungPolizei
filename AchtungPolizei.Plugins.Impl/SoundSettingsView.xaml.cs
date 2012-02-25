using System;
using System.Windows.Controls;

namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Interaction logic for SoundSettingsView.xaml
    /// </summary>
    public partial class SoundSettingsView : UserControl, IConfigirationControl
    {
        public SoundSettingsView()
        {
            InitializeComponent();
        }

        public bool Validate()
        {
            return Model.Error == null;
        }

        public ConfigurationBase GetConfiguration()
        {
            return new SoundPluginConfiguration
                       {
                           Broken = Model.BrokenFile,
                           Fixed = Model.FixedFile,
                           StillBroken = Model.StillBrokenFile
                       };
        }

        public Type GetConfigurationType()
        {
            return typeof (SoundPluginConfiguration);
        }

        private SoundSettingsModel Model
        {
            get
            {
                return (SoundSettingsModel) DataContext;
            }
        }
    }
}
