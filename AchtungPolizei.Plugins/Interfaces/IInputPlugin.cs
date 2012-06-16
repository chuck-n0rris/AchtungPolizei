namespace AchtungPolizei.Plugins.Interfaces
{
    /// <summary>
    /// Interface to be implemented by plugin components
    /// </summary>
    public interface IInputPlugin : IPlugin
    {
        /// <summary>
        /// Launches the plugin.
        /// </summary>
        void Launch();
    }
}