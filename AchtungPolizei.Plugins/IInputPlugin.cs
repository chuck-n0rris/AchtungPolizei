using System;

namespace AchtungPolizei.Plugins
{
    public interface IInputPlugin : IPlugin
    {
        event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}