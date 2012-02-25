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

        protected int GetParameter(string key, int defaultValue)
        {
            return Parameters.ContainsKey(key) ? int.Parse(Parameters[key]) : defaultValue;
        }

        protected string GetParameter(string key, string defaultValue)
        {
            return Parameters.ContainsKey(key) ? Parameters[key] : defaultValue;
        }
    }
}