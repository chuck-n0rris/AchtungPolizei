using System;
using System.Collections.Generic;

namespace Altalerta.Core
{
    public interface IPlugin : IDisposable
    {
        string Name { get; }
        IDictionary<string, string> Configuration { get; set; }
    }
}