namespace AchtungPolizei.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// Bunch input pluing state to output plugin.
    /// </summary>
    public class StateBinder
    {
        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        public string StateId { get; set; }

        /// <summary>
        /// Gets or sets the output plugins information.
        /// </summary>
        public List<OutputPluginInfo> OutputPlugins { get; set; }
    }
}