using System;
using System.Collections.Generic;

namespace AchtungPolizei.Plugins
{
    public class ConfigurationBase
    {
        public ConfigurationBase()
        {
            parameters = new Dictionary<string, string>();
        }

        [NonSerialized] 
        protected IDictionary<string, string> parameters;

        protected int GetParameter(string key, int defaultValue)
        {
            return parameters.ContainsKey(key) ? int.Parse(parameters[key]) : defaultValue;
        }

        protected string GetParameter(string key, string defaultValue)
        {
            return parameters.ContainsKey(key) ? parameters[key] : defaultValue;
        }
    }
}