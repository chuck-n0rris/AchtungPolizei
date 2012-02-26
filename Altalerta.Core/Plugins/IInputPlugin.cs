using System;
using Altalerta.Core.Essential;

namespace Altalerta.Core.Plugins
{
    public interface IInputPlugin : IPlugin
    {
        event EventHandler<BuildInfoReceivedEventArgs> BuildInfoReceived;
    }
}