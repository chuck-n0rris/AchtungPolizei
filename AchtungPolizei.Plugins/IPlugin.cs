using System.Windows.Controls;
using System;

namespace AchtungPolizei.Plugins
{
    public interface IPlugin : IDisposable
    {
        Guid Id { get; }

        string Name { get; }

        ConfigurationBase Configuration { get; set; }

        Control GetConfigControl();
    }
}