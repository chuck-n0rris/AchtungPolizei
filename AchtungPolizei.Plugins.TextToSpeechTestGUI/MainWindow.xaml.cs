using System.Windows;
using AchtungPolizei.Plugins.Impl;
using AchtungPolizei.Plugins.TextToSpeech;

namespace AchtungPolizei.Plugins.TextToSpeechTestGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConfigirationControl settingsControl;
        public MainWindow()
        {
            InitializeComponent();

            var plugin = new LightOutputPlugin();
            settingsControl = plugin.GetConfigControl();

            ContentControl.Content = settingsControl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(settingsControl.Validate() ? "Valid" : "Invalid");
            var configuration = settingsControl.GetConfiguration();
        }
    }
}
