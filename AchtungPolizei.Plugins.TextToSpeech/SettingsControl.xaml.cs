using System;

namespace AchtungPolizei.Plugins.TextToSpeech
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl, IConfigirationControl
    {
        private readonly SettingsViewModel viewModel;

        public SettingsControl()
        {
            InitializeComponent();

            viewModel = new SettingsViewModel();
            DataContext = viewModel;
        }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(viewModel.BuildBrokenPhrase) &&
                   !string.IsNullOrEmpty(viewModel.BuildStillBrokenPhrase) &&
                   !string.IsNullOrEmpty(viewModel.BuildFixedPhrase);
        }

        public ConfigurationBase GetConfiguration()
        {
            return new TextToSpeechConfiguration
                {
                    BuildBrokenPhrase = viewModel.BuildBrokenPhrase,
                    BuildFixedPhrase = viewModel.BuildFixedPhrase,
                    BuildStillBrokenPhrase = viewModel.BuildStillBrokenPhrase
                };
        }

        public Type GetConfigurationType()
        {
            return typeof(TextToSpeechConfiguration);
        }
    }
}
