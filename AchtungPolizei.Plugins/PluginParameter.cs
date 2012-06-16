namespace AchtungPolizei.Plugins
{
    /// <summary>
    /// Used for configure output and input plugins.
    /// </summary>
    public class PluginParameter
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        public ParameterType Type { get; set; }
    }
}