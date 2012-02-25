namespace AchtungPolizei.Plugins
{
    using System;

    public interface IPlugin : IDisposable
    {
        Guid Id { get; }

        string Name { get; }

        ConfigurationBase Configuration { get; set; }

        IConfigirationControl GetConfigControl();

        event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}