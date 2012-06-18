namespace AchtungPolizei.Plugins
{
    using System;

    /// <summary>
    /// Containing the data of plugin state changed event.
    /// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangedEventArgs"/> class. 
        /// </summary>
        /// <param name="newState">
        /// The new plugin state.
        /// </param>
        /// <param name="previousState">
        /// The previous plugin state.
        /// </param>
        public StateChangedEventArgs(PluginState newState, PluginState previousState)
        {
            this.NewState = newState;
            this.PreviousState = previousState;
        }

        /// <summary>
        /// Gets the new plugin state.
        /// </summary>
        public PluginState NewState { get; private set; }

        /// <summary>
        /// Gets the previous plugin state.
        /// </summary>
        public PluginState PreviousState { get; private set; }
    }
}