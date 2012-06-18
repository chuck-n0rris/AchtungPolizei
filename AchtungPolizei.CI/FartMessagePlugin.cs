namespace AchtungPolizei.CI
{
    using System.Windows;
    using AchtungPolizei.Plugins.Interfaces;

    public class FartMessagePlugin : IOutputPlugin
    {
        /// <summary>
        /// Gets the plugin identifier.
        /// </summary>
        public string Id
        {
            get
            {
                return "5935453b-8c8a-461f-bf7d-fd9386558685";
            }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        public string Name
        {
            get
            {
                return "FartMessage";
            }
        }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        public string Description
        {
            get
            {
                return "Fart Message Plug-in";
            }
        }

        /// <summary>
        /// Executes the output plugin logic.
        /// </summary>
        public void Execute()
        {
            MessageBox.Show("Hello.. пук");
        }
    }
}