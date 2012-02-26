using System;

namespace Altalerta.Core
{
    public interface IInputPlugin : IPlugin
    {
        event EventHandler<BuildInfoReceivedEventArgs> BuildInfoReceived;
    }
}