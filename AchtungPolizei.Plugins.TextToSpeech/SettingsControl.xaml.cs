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

        private readonly TextToSpeechConfiguration configuration;

        public SettingsControl(TextToSpeechConfiguration configuration)
        {
            InitializeComponent();

            this.configuration = configuration;

            viewModel = new SettingsViewModel
                            {
                                BuildBrokenPhrase = configuration.BuildBrokenPhrase,
                                BuildStillBrokenPhrase = configuration.BuildStillBrokenPhrase,
                                BuildFixedPhrase = configuration.BuildFixedPhrase
                            };

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
            configuration.BuildBrokenPhrase = viewModel.BuildBrokenPhrase;
            configuration.BuildFixedPhrase = viewModel.BuildFixedPhrase;
            configuration.BuildStillBrokenPhrase = viewModel.BuildStillBrokenPhrase;

            return configuration;
        }

        public Type GetConfigurationType()
        {
            return typeof(TextToSpeechConfiguration);
        }
    }
}
