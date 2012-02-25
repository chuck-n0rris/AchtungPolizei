namespace AchtungPolizei.Tray
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    using AchtungPolizei.Core.Helpers;
    using AchtungPolizei.Plugins;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Applications the startup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="System.Windows.StartupEventArgs"/> 
        /// instance containing the event data.
        /// </param>
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Hide();

            var assemblyFile = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string assemblyDirectory = assemblyFile.Directory.FullName;
            string pluginsDirectory = Path.Combine(assemblyDirectory, "Plugins");

            PluginLocator.Initialize(pluginsDirectory);
        }
    }
}
