using System;
using System.Windows.Controls;

namespace AchtungPolizei.Plugins.TeamFoundation
{
    /// <summary>
    /// Interaction logic for TeamFoundationSettingsControl.xaml
    /// </summary>
    public partial class TeamFoundationSettingsControl : UserControl, IConfigirationControl
    {
        private TeamFoundationConfiguration configuration;

        public TeamFoundationSettingsControl(TeamFoundationConfiguration configuration)
        {
            InitializeComponent();

            this.configuration = configuration;
        }

        public bool Validate()
        {
            return true;
        }

        public ConfigurationBase GetConfiguration()
        {
            return configuration;
        }

        public Type GetConfigurationType()
        {
            return typeof(TeamFoundationConfiguration);
        }
    }
}
