using System;

namespace AchtungPolizei.Plugins
{
    public interface IInputPlugin : IPlugin
    {
        IConfigirationControl GetConfigControl();

        ConfigurationBase Configuration { get; set; }

        event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}