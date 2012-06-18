namespace AchtungPolizei.Core
{
    using AchtungPolizei.Plugins.Interfaces;

    /// <summary>
    /// Interface to be implemented by plugins manager components.
    /// </summary>
    public interface IPluginsManager
    {
        /// <summary>
        /// Gets the output plugin by plugin identifier.
        /// </summary>
        /// <param name="id">
        /// Requested plugin identifier.
        /// </param>
        /// <returns>
        /// The instance of requested plug-in.
        /// </returns>
        IOutputPlugin GetOutputPlugin(string id);

        /// <summary>
        /// Gets the intput plugin by plugin identifier.
        /// </summary>
        /// <param name="id">
        /// Requested plugin identifier.
        /// </param>
        /// <returns>
        /// The instance of requested plug-in.
        /// </returns>
        IInputPlugin GetInputPlugin(string id);
    }
}