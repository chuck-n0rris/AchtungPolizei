using System;

namespace AchtungPolizei.Plugins
{
    using System.Windows.Controls;

    public interface IInputPlugin : IPlugin
    {
        Control GetConfigControl();

        ConfigurationBase Configuration { get; set; }

        event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}