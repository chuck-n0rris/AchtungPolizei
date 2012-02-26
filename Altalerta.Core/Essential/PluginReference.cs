using System.Collections.Generic;

namespace Altalerta.Core.Essential
{
    public class PluginReference
    {
        public string Name { get; set; }
        public IDictionary<string, string> Configuration { get; set; }
    }
}