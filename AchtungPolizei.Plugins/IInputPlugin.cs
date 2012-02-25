namespace AchtungPolizei.Plugins
{
    using System;

    /// <summary>
    /// Defines interface for input plugin.
    /// </summary>
    public interface IInputPlugin : IPlugin
    {
        /// <summary>
        /// Occurs when build status is received from CI.
        /// </summary>
        event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}