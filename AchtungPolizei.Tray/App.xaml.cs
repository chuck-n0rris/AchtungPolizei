using AchtungPolizei.Core;

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
            var engine = Engine.Current;

            PluginLocator.Initialize(engine.GetPluginDirectory());
            engine.Start();

            var mainWindow = new MainWindow();
            mainWindow.Hide();
        }
    }
}
