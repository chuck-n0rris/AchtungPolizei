namespace AchtungPolizei.Plugins.Interfaces
{
    /// <summary>
    /// Interface to be implemented by output plugin components.
    /// </summary>
    public interface IOutputPlugin : IPlugin
    {
        /// <summary>
        /// Executes the output plugin logic.
        /// </summary>
        void Execute();
    }
}