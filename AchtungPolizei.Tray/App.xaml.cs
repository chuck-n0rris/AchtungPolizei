namespace AchtungPolizei.Tray
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    using Hardcodet.Wpf.TaskbarNotification;

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
        }
    }
}
