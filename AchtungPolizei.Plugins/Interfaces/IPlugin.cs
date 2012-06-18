namespace AchtungPolizei.Plugins.Interfaces
{
    /// <summary>
    /// Interface to be implemented by plugin components.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        string Description { get; }
    }
}