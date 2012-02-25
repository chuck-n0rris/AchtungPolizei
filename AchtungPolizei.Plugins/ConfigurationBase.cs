using System.Collections.Generic;

namespace AchtungPolizei.Plugins
{
    public class ConfigurationBase
    {
        public ConfigurationBase()
        {
            Parameters = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Parameters { get; set; }
    }
}