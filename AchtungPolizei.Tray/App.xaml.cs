namespace AchtungPolizei.Tray
{
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using AchtungPolizei.CI;
    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void ApplicationStartup(object sender, System.Windows.StartupEventArgs e)
        {
            var inputPlugin = new TeamCityPlugin();
            var outputPlugin = new FartMessagePlugin();

            var configuration = new Configuration();

            configuration.InputPluginId = inputPlugin.Id;
            configuration.Name = "Test";
            configuration.InputPluginSettings = new TeamCitySettingsModel
            {
                Url = "www.test.com",
                UserName = "user",
                Password = "password"
            };

            var outputInfo = new OutputPluginInfo { Id = outputPlugin.Id };
            outputInfo.Settings = new SettingsModel();

            configuration.StateBinders = (new[] 
            { 
                new StateBinder
                {
                    StateId = "someState",
                    OutputPlugins = (new[] { outputInfo }).ToList()
                }
            })
            .ToList();

            var x = new XmlSerializer(configuration.GetType(),  new[] { configuration.InputPluginSettings.GetType() });
            var stream = File.OpenWrite("d:\\test.txt");

            x.Serialize(stream, configuration);
        }
    }
}
